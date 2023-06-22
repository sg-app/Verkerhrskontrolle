using Microsoft.EntityFrameworkCore;
using Verkehrskontrolle.Extensions;
using Verkehrskontrolle.Models;

namespace Verkehrskontrolle.Data
{
    public class AuthDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User() 
                { 
                    Email = "admin@conti.de", 
                    Password = "admin".CreateMD5()
                });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
