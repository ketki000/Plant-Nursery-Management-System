using Microsoft.EntityFrameworkCore;
using PlantNurseryAssessment.Models;

namespace PlantNurseryAssessment.Data
{
    public class NurseryContext : DbContext
    {
       
        public NurseryContext(DbContextOptions<NurseryContext> options) : base(options) { }
        

        public DbSet<Plant> Plants { get; set; } = default!;
        public DbSet<Purchase> Purchases { get; set; } = default!;
        public DbSet<Customer> Customers { get; set; } = default!;
    }
}
