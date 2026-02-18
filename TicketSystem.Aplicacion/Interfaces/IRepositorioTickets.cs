using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Aplicacion.DTOs;
using TicketSystem.Dominio.Entidades;

namespace TicketSystem.Aplicacion.Interfaces
{
    public interface IRepositorioTickets
    {
        Task CrearAsync(Ticket ticket);
        Task<IEnumerable<Ticket>> ObtenerPorUsuarioAsync(Guid usuarioId);

        Task<Ticket?> ObtenerPorIdAsync(Guid id);
        IQueryable<Ticket> ObtenerQueryable();

        Task<(IEnumerable<Ticket> Tickets, int Total)> ObtenerFiltradosAsync(FiltroTicketsDto filtro);

        Task GuardarCambiosAsync();
       
        Task ActualizarAsync(Ticket ticket);
    }
}
