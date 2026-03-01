using System;

namespace TicketSystem.Aplicacion.DTOs
{
    public class AsignarOperadorDto
    {
        public Guid TicketId { get; set; }
        public Guid? OperadorId { get; set; }
    }
}
