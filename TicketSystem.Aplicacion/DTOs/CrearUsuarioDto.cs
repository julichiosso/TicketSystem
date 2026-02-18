using TicketSystem.Dominio.Enumeraciones;

namespace TicketSystem.Aplicacion.DTOs
{
    public class CrearUsuarioDto
    {
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public RolUsuario Rol { get; set; }
    }
}
