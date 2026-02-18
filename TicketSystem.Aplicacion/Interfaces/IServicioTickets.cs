using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Aplicacion.Common;
using TicketSystem.Aplicacion.DTOs;
using TicketSystem.Dominio.Enumeraciones;

namespace TicketSystem.Aplicacion.Interfaces
{
    public interface IServicioTickets
    {
        Task<Guid> CrearAsync(CrearTicketDto dto);
        Task<IEnumerable<TicketDto>> ObtenerPorUsuarioAsync(Guid usuarioId);
        Task CambiarEstadoAsync(Guid ticketId, EstadoTicket nuevoEstado);

        Task<PagedResult<TicketDto>> ObtenerFiltradosAsync(FiltroTicketsDto filtro);

        Task EliminarAsync(Guid id);

    }
}
