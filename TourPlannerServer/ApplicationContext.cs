using Microsoft.EntityFrameworkCore;
using TourPlannerServer.Model;

namespace TourPlannerServer {
    public class ApplicationContext : DbContext {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {

        }

        public DbSet<Tour> Tour { get; set; }
        public DbSet<TourLog> TourLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Tour>()
                .HasMany(t => t.TourLogs)
                .WithOne(t1 => t1.Tour)
                .HasForeignKey(t => t.TourId);
        }
    }
}
