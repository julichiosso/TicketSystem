using TicketSystem.Aplicacion.DTOs;
using TicketSystem.Aplicacion.Interfaces;
using TicketSystem.Dominio.Entidades;

namespace TicketSystem.Aplicacion.Servicios
{
    public class ServicioUsuarios : IServicioUsuarios
    {
        private readonly IRepositorioUsuarios _repositorio;

        public ServicioUsuarios(IRepositorioUsuarios repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Guid> CrearAsync(CrearUsuarioDto dto)
        {
            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nombre = dto.Nombre,
                Email = dto.Email,
                Rol = dto.Rol
            };

            await _repositorio.CrearAsync(usuario);
            return usuario.Id;
        }

        public async Task<IEnumerable<UsuarioDto>> ObtenerTodosAsync()
        {
            var usuarios = await _repositorio.ObtenerTodosAsync();

            return usuarios.Select(u => new UsuarioDto
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Email = u.Email,
                Rol = u.Rol
            });
        }

        public async Task<UsuarioDto?> ObtenerPorIdAsync(Guid id)
        {
            var usuario = await _repositorio.ObtenerPorIdAsync(id);

            if (usuario == null)
                return null;

            return new UsuarioDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                Rol = usuario.Rol
            };
        }

        public async Task EliminarAsync(Guid id)
        {
            await _repositorio.EliminarAsync(id);
        }
    }
}
