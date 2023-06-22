using Microsoft.EntityFrameworkCore;
using Verkehrskontrolle.Models;

namespace Verkehrskontrolle.Data
{
    public class VerkehrskontrolleDbContext : DbContext
    {

        public virtual DbSet<Fahrzeug> Fahrzeuge { get; set; }
        public virtual DbSet<Halter> Halter { get; set; }
        public virtual DbSet<Führerschein> Fuehrerscheine { get; set; }

        public VerkehrskontrolleDbContext(DbContextOptions<VerkehrskontrolleDbContext> options): base(options)
        {
            
        }
        public VerkehrskontrolleDbContext() { }
    }
}
