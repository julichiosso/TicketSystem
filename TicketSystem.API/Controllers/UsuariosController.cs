using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Aplicacion.Interfaces;
using TicketSystem.Aplicacion.DTOs;
using TicketSystem.Dominio.Enumeraciones;

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

        return Ok(new
        {
            success = true,
            data = resultado
        });
    }


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

        return Ok(new
        {
            success = true,
            data = resultado
        });
    }

    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsuario(Guid id)
    {
        var usuario = await _repositorioUsuarios.ObtenerPorIdAsync(id);

        if (usuario == null)
            throw new KeyNotFoundException("Usuario no encontrado");

        await _repositorioUsuarios.EliminarAsync(usuario);

        return NoContent();
    }

    [HttpPut("roles")]
    public async Task<IActionResult> UpdateRoles([FromBody] List<ActualizarRolDto> cambios)
    {
        if (cambios == null || cambios.Count == 0)
            return BadRequest(new { message = "No se proporcionaron cambios." });

        foreach (var cambio in cambios)
        {
            var usuario = await _repositorioUsuarios.ObtenerPorIdAsync(cambio.Id);
            if (usuario == null)
                return NotFound(new { message = $"Usuario {cambio.Id} no encontrado." });

            if (!Enum.IsDefined(typeof(RolUsuario), cambio.Rol))
                return BadRequest(new { message = $"Rol inválido para usuario {cambio.Id}." });

            usuario.Rol = (RolUsuario)cambio.Rol;
            await _repositorioUsuarios.ActualizarAsync(usuario);
        }

        return Ok(new { success = true, message = "Roles actualizados correctamente." });
    }
}
