using Microsoft.EntityFrameworkCore;
using TesteVolvo.Models;

namespace TesteVolvo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
        }       

        //protected override void OnModelCreating(Modelbuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Company>()
        //        .HasMany(c => c.Employees)
        //        .WithOne(e => e.Company).
        
        //        .IsRequired();
        //}

        public DbSet<Truck> Trucks { get; set; }
        public DbSet<TruckModel> TruckModels { get; set; }

    }
}
