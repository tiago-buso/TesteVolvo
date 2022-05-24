using Microsoft.EntityFrameworkCore;
using TesteVolvo.Models;

namespace TesteVolvo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
        }       

        public DbSet<Truck> Trucks { get; set; }
        public DbSet<TruckModel> TruckModels { get; set; }
        public DbSet<BaseTruckModel> BaseTruckModels { get; set; }

    }
}
