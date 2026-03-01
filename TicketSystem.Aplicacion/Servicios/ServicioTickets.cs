using TicketSystem.Aplicacion.Common;
using TicketSystem.Aplicacion.DTOs;
using TicketSystem.Aplicacion.Interfaces;
using TicketSystem.Dominio.Entidades;
using TicketSystem.Dominio.Enumeraciones;
using Microsoft.EntityFrameworkCore;

namespace TicketSystem.Aplicacion.Servicios
{
    public class ServicioTickets : IServicioTickets
    {
        private readonly IRepositorioTickets _repositorioTickets;
        private readonly IRepositorioUsuarios _repositorioUsuarios;

        public ServicioTickets(
            IRepositorioTickets repositorioTickets,
            IRepositorioUsuarios repositorioUsuarios)
        {
            _repositorioTickets = repositorioTickets;
            _repositorioUsuarios = repositorioUsuarios;
        }

        public async Task<Guid> CrearAsync(CrearTicketDto dto)
        {
            var usuario = await _repositorioUsuarios.ObtenerPorIdAsync(dto.UsuarioId);

            if (usuario == null)
                throw new Exception("El usuario no existe");

            var fechaCreacion = DateTime.UtcNow;
            
            // SLA Calculation
            DateTime? fechaLimite = dto.Prioridad switch
            {
                PrioridadTicket.Alta => fechaCreacion.AddHours(4),
                PrioridadTicket.Media => fechaCreacion.AddHours(24),
                PrioridadTicket.Baja => fechaCreacion.AddHours(48),
                _ => (DateTime?)null
            };

            var ticket = new Ticket
            {
                Id = Guid.NewGuid(),
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                Estado = EstadoTicket.Pendiente,
                Prioridad = dto.Prioridad,
                UsuarioId = dto.UsuarioId,
                FechaCreacion = fechaCreacion,
                FechaLimite = fechaLimite,
                SLACumplido = true
            };

            await _repositorioTickets.CrearAsync(ticket);
            
            await RegistrarAccionAsync(ticket.Id, dto.UsuarioId, "CREACIÓN", $"Ticket '{ticket.Titulo}' creado por {usuario.Nombre}.");
            
            return ticket.Id;
        }

        public async Task<IEnumerable<TicketDto>> ObtenerPorUsuarioAsync(Guid usuarioId)
        {
            var tickets = await _repositorioTickets.ObtenerPorUsuarioAsync(usuarioId);

            return tickets.Select(t => new TicketDto
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descripcion = t.Descripcion,
                Estado = t.Estado,
                Prioridad = t.Prioridad,
                FechaCreacion = t.FechaCreacion,
                UsuarioNombre = t.Usuario?.Nombre,
                OperadorAsignadoNombre = t.OperadorAsignado?.Nombre
            });
        }

        private bool EsTransicionValida(EstadoTicket actual, EstadoTicket nuevo)
        {
            return (actual, nuevo) switch
            {
                (EstadoTicket.Pendiente, EstadoTicket.EnProceso) => true,
                (EstadoTicket.EnProceso, EstadoTicket.EsperandoUsuario) => true,
                (EstadoTicket.EsperandoUsuario, EstadoTicket.EnProceso) => true,
                (EstadoTicket.EnProceso, EstadoTicket.Resuelto) => true,
                (EstadoTicket.EsperandoUsuario, EstadoTicket.Resuelto) => true,
                (EstadoTicket.Resuelto, EstadoTicket.Cerrado) => true,
                _ => false
            };
        }

        public async Task CambiarEstadoAsync(Guid ticketId, EstadoTicket nuevoEstado)
        {
            var ticket = await _repositorioTickets.ObtenerPorIdAsync(ticketId);

            if (ticket == null)
                throw new KeyNotFoundException("El ticket no existe");

            if (!EsTransicionValida(ticket.Estado, nuevoEstado))
                throw new ArgumentException($"No se puede cambiar de {ticket.Estado} a {nuevoEstado}");

            var estadoAnterior = ticket.Estado;
            ticket.Estado = nuevoEstado;

            // Check SLA on resolution
            if (nuevoEstado == EstadoTicket.Resuelto && ticket.FechaLimite.HasValue)
            {
                ticket.SLACumplido = DateTime.UtcNow <= ticket.FechaLimite.Value;
            }

            await _repositorioTickets.GuardarCambiosAsync();
            
            await RegistrarAccionAsync(ticketId, Guid.Empty, "ESTADO_CAMBIO", $"Cambio de estado de {estadoAnterior} a {nuevoEstado}.");
        }

        public async Task<PagedResult<TicketDto>> ObtenerFiltradosAsync(FiltroTicketsDto filtro)
        {
            // Use repository method that already includes the `Usuario` navigation
            var (tickets, total) = await _repositorioTickets.ObtenerFiltradosAsync(filtro);

            return new PagedResult<TicketDto>
            {
                Data = tickets.Select(t => new TicketDto
                {
                    Id = t.Id,
                    Titulo = t.Titulo,
                    Descripcion = t.Descripcion,
                    Estado = t.Estado,
                    Prioridad = t.Prioridad,
                    FechaCreacion = t.FechaCreacion,
                    UsuarioNombre = t.Usuario?.Nombre,
                    OperadorAsignadoNombre = t.OperadorAsignado?.Nombre
                }),
                Page = filtro.Page,
                PageSize = filtro.PageSize,
                TotalRecords = total
            };
        }

        public async Task EliminarAsync(Guid id)
        {
            var ticket = await _repositorioTickets.ObtenerPorIdAsync(id);

            if (ticket == null)
                throw new Exception("Ticket no encontrado");

            ticket.IsDeleted = true;
            await _repositorioTickets.GuardarCambiosAsync();
            
            await RegistrarAccionAsync(id, Guid.Empty, "DELECIÓN", $"Ticket '{ticket.Titulo}' eliminado.");
        }

        public async Task<IEnumerable<ComentarioTicketDto>> ObtenerComentariosAsync(Guid ticketId, bool incluirInternos)
        {
            var ticket = await _repositorioTickets.ObtenerPorIdAsync(ticketId);
            if (ticket == null) throw new KeyNotFoundException("El ticket no existe");

            var comentarios = await _repositorioTickets.ObtenerComentariosPorTicketAsync(ticketId, incluirInternos);

            return comentarios.Select(c => new ComentarioTicketDto
            {
                Id = c.Id,
                TicketId = c.TicketId,
                AutorId = c.AutorId,
                Autor = c.Autor?.Nombre ?? "Sistema",
                Rol = (int)(c.Autor?.Rol ?? RolUsuario.Usuario),
                Mensaje = c.Mensaje,
                Interno = c.EsInterno,
                Fecha = c.FechaCreacion
            });
        }

        public async Task<ComentarioTicketDto> AgregarComentarioAsync(Guid ticketId, Guid autorId, CrearComentarioTicketDto dto)
        {
            var ticket = await _repositorioTickets.ObtenerPorIdAsync(ticketId);
            if (ticket == null) throw new KeyNotFoundException("El ticket no existe");

            var autor = await _repositorioUsuarios.ObtenerPorIdAsync(autorId);
            if (autor == null) throw new KeyNotFoundException("El autor no existe");

            var comentario = new ComentarioTicket
            {
                Id = Guid.NewGuid(),
                TicketId = ticketId,
                AutorId = autorId,
                Mensaje = dto.Mensaje,
                EsInterno = dto.Interno,
                FechaCreacion = DateTime.UtcNow
            };

            await _repositorioTickets.AgregarComentarioAsync(comentario);
            
            await RegistrarAccionAsync(ticketId, autorId, "COMENTARIO", $"{(dto.Interno ? "[Interno] " : "")}Comentario agregado por {autor.Nombre}.");

            return new ComentarioTicketDto
            {
                Id = comentario.Id,
                TicketId = comentario.TicketId,
                AutorId = comentario.AutorId,
                Autor = autor.Nombre,
                Rol = (int)autor.Rol,
                Mensaje = comentario.Mensaje,
                Interno = comentario.EsInterno,
                Fecha = comentario.FechaCreacion
            };
        }

        public async Task<IEnumerable<TicketDto>> ObtenerPorOperadorAsync(Guid operadorId)
        {
            var tickets = await _repositorioTickets.ObtenerPorOperadorAsync(operadorId);

            return tickets.Select(t => new TicketDto
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descripcion = t.Descripcion,
                Estado = t.Estado,
                Prioridad = t.Prioridad,
                FechaCreacion = t.FechaCreacion,
                UsuarioNombre = t.Usuario?.Nombre,
                OperadorAsignadoNombre = t.OperadorAsignado?.Nombre
            });
        }

        public async Task AsignarOperadorAsync(Guid ticketId, Guid? operadorId)
        {
            var ticket = await _repositorioTickets.ObtenerPorIdAsync(ticketId);

            if (ticket == null)
                throw new KeyNotFoundException("El ticket no existe");

            string nombreOperador = "Ninguno";
            if (operadorId.HasValue)
            {
                var operador = await _repositorioUsuarios.ObtenerPorIdAsync(operadorId.Value);
                if (operador == null)
                    throw new KeyNotFoundException("El operador no existe");

                if (operador.Rol != RolUsuario.Operador && operador.Rol != RolUsuario.Administrador)
                    throw new ArgumentException("El usuario no es un operador válido");
                
                nombreOperador = operador.Nombre;
            }

            ticket.OperadorAsignadoId = operadorId;
            await _repositorioTickets.ActualizarAsync(ticket);
            
            await RegistrarAccionAsync(ticketId, Guid.Empty, "ASIGNACIÓN", $"Operador asignado: {nombreOperador}.");
        }

        private async Task RegistrarAccionAsync(Guid? ticketId, Guid? usuarioId, string accion, string detalle)
        {
            await _repositorioTickets.RegistrarAuditLogAsync(new AuditLog
            {
                Id = Guid.NewGuid(),
                TicketId = ticketId,
                UsuarioId = usuarioId,
                Accion = accion,
                Detalle = detalle,
                Fecha = DateTime.UtcNow
            });
        }

        public async Task<IEnumerable<AuditLog>> ObtenerAuditLogsAsync()
        {
            return await _repositorioTickets.ObtenerAuditLogsAsync();
        }

        public async Task<MetricasDto> ObtenerMetricasAsync()
        {
            var tickets = await _repositorioTickets.ObtenerQueryable().ToListAsync();
            
            var resueltos = tickets.Where(t => t.Estado == EstadoTicket.Resuelto).ToList();
            var totalResueltos = resueltos.Count;
            
            int resueltosHoy = resueltos.Count(t => t.FechaCreacion.Date == DateTime.UtcNow.Date);
            
            double slaCumplido = totalResueltos > 0 
                ? (double)resueltos.Count(t => t.SLACumplido) / totalResueltos * 100 
                : 100;

            // Simple avg resolution time calculation
            string avgTime = "0h 0m";
            if (totalResueltos > 0)
            {
                var durations = resueltos.Select(t => (DateTime.UtcNow - t.FechaCreacion).TotalHours);
                var avgHours = durations.Average();
                avgTime = $"{(int)avgHours}h {(int)((avgHours - (int)avgHours) * 60)}m";
            }

            return new MetricasDto
            {
                TotalTickets = tickets.Count,
                TicketsPendientes = tickets.Count(t => t.Estado == EstadoTicket.Pendiente),
                TicketsEnProceso = tickets.Count(t => t.Estado == EstadoTicket.EnProceso),
                TicketsResueltos = totalResueltos,
                TicketsEsperandoUsuario = tickets.Count(t => t.Estado == EstadoTicket.EsperandoUsuario),
                ResueltosHoy = resueltosHoy,
                PorcentajeSlaCumplido = Math.Round(slaCumplido, 1),
                TiempoPromedioResolucion = avgTime,
                DistribucionPorEstado = tickets.GroupBy(t => t.Estado)
                    .ToDictionary(g => g.Key.ToString(), g => g.Count())
            };
        }
    }
}
