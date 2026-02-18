using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Aplicacion.Interfaces;

namespace TicketSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrador")]
public class UsuariosController : ControllerBase
{
    private readonly IRepositorioUsuarios _repositorioUsuarios;

    public UsuariosController(IRepositorioUsuarios repositorioUsuarios)
    {
        _repositorioUsuarios = repositorioUsuarios;
    }


  

    // GET: api/Usuarios
    [HttpGet]
    public async Task<IActionResult> GetUsuarios()
    {
        var usuarios = await _repositorioUsuarios.ObtenerTodosAsync();

        var resultado = usuarios.Select(u => new
        {
            u.Id,
            u.Nombre,
            u.Email,
            u.Rol
        });
        throw new Exception("Error de prueba");
        return Ok(resultado);
    }

    // GET: api/Usuarios/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUsuario(Guid id)
    {
        var usuario = await _repositorioUsuarios.ObtenerPorIdAsync(id);

        if (usuario == null)
            throw new KeyNotFoundException("Usuario no encontrado");

        var resultado = new
        {
            usuario.Id,
            usuario.Nombre,
            usuario.Email,
            usuario.Rol
        };

        return Ok(resultado);
    }

    // DELETE: api/Usuarios/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsuario(Guid id)
    {
        var usuario = await _repositorioUsuarios.ObtenerPorIdAsync(id);

        if (usuario == null)
            throw new KeyNotFoundException("Usuario no encontrado");

        await _repositorioUsuarios.EliminarAsync(usuario);

        return NoContent();
    }
}
