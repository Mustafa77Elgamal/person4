using Microsoft.EntityFrameworkCore;
using person_4.Models;

namespace person_4.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<MacroCalculation> MacroCalculations { get; set; }

        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Review>().HasIndex(x => x.SubscriptionId).IsUnique();
        }
    }
}
