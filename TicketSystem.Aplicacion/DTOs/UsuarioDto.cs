using TicketSystem.Dominio.Enumeraciones;

namespace TicketSystem.Aplicacion.DTOs
{
    public class UsuarioDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public RolUsuario Rol { get; set; }
    }
}
