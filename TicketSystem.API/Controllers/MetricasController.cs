using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Aplicacion.Interfaces;

namespace TicketSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrador,Operador")]
public class MetricasController : ControllerBase
{
    private readonly IServicioTickets _servicioTickets;

    public MetricasController(IServicioTickets servicioTickets)
    {
        _servicioTickets = servicioTickets;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerMetricas()
    {
        var metricas = await _servicioTickets.ObtenerMetricasAsync();
        return Ok(new { success = true, data = metricas });
    }
}
