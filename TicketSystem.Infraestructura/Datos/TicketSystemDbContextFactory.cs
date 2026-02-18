using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TicketSystem.Infraestructura.Datos
{
    public class TicketSystemDbContextFactory
        : IDesignTimeDbContextFactory<TicketSystemDbContext>
    {
        public TicketSystemDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TicketSystemDbContext>();
            
            optionsBuilder.UseSqlite("Data Source=ticketsystem.db");

            return new TicketSystemDbContext(optionsBuilder.Options);
        }
    }
}
