using TicketSystem.Aplicacion.DTOs;
using TicketSystem.Dominio.Entidades;

namespace TicketSystem.Aplicacion.Interfaces
{
    public interface IRepositorioTickets
    {
        Task CrearAsync(Ticket ticket);
        Task<IEnumerable<Ticket>> ObtenerPorUsuarioAsync(Guid usuarioId);
        Task<IEnumerable<Ticket>> ObtenerPorOperadorAsync(Guid operadorId);
        Task<Ticket?> ObtenerPorIdAsync(Guid id);
        IQueryable<Ticket> ObtenerQueryable();
        Task<(IEnumerable<Ticket> Tickets, int Total)> ObtenerFiltradosAsync(FiltroTicketsDto filtro);
        Task GuardarCambiosAsync();
        Task ActualizarAsync(Ticket ticket);

        Task<IEnumerable<ComentarioTicket>> ObtenerComentariosPorTicketAsync(Guid ticketId, bool incluirInternos);
        Task AgregarComentarioAsync(ComentarioTicket comentario);
        Task RegistrarAuditLogAsync(AuditLog log);
        Task<IEnumerable<AuditLog>> ObtenerAuditLogsAsync();
    }
}
