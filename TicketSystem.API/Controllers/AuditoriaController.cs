using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Aplicacion.Interfaces;

namespace TicketSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrador")]
public class AuditoriaController : ControllerBase
{
    private readonly IServicioTickets _servicioTickets;

    public AuditoriaController(IServicioTickets servicioTickets)
    {
        _servicioTickets = servicioTickets;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerLogs()
    {
        var logs = await _servicioTickets.ObtenerAuditLogsAsync();
        return Ok(new { success = true, data = logs });
    }
}
