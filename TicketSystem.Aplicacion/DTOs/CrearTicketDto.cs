using TicketSystem.Dominio.Enumeraciones;

namespace TicketSystem.Aplicacion.DTOs
{
    public class CrearTicketDto
    {
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public Guid UsuarioId { get; set; }
        public PrioridadTicket Prioridad { get; set; }
    }
}