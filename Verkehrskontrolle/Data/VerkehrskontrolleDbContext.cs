using Microsoft.EntityFrameworkCore;
using Verkehrskontrolle.Models;

namespace Verkehrskontrolle.Data
{
    public class VerkehrskontrolleDbContext : DbContext
    {

        public DbSet<Fahrzeug> Fahrzeuge { get; set; }
        public DbSet<Halter> Halter { get; set; }
        public DbSet<Führerschein> Fuehrerscheine { get; set; }

        public VerkehrskontrolleDbContext(DbContextOptions options): base(options)
        {
            
        }
    }
}
