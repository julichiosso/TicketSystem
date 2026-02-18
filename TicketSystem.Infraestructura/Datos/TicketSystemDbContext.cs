using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Dominio.Entidades;

namespace TicketSystem.Infraestructura.Datos
{
    public class TicketSystemDbContext : DbContext
    {
        public TicketSystemDbContext(DbContextOptions<TicketSystemDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
