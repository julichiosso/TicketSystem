using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Dominio.Enumeraciones;

namespace TicketSystem.Dominio.Entidades
{
    public class Ticket
    {
        public Guid Id { get; set; }

        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;

        public PrioridadTicket Prioridad { get; set; }
        public EstadoTicket Estado { get; set; } = EstadoTicket.Pendiente;


        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
