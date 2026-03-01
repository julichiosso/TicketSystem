using System;
using System.Collections.Generic;

namespace TicketSystem.Aplicacion.DTOs
{
    public class MetricasDto
    {
        public int TotalTickets { get; set; }
        public int TicketsPendientes { get; set; }
        public int TicketsEnProceso { get; set; }
        public int TicketsResueltos { get; set; }
        public int TicketsEsperandoUsuario { get; set; }
        
        public double PorcentajeSlaCumplido { get; set; }
        public string TiempoPromedioResolucion { get; set; } = "0h 0m";
        
        public Dictionary<string, int> DistribucionPorEstado { get; set; } = new();
        public int ResueltosHoy { get; set; }
    }
}
