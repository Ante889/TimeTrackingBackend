using Microsoft.EntityFrameworkCore;
using TimeTracking.App.Category.Domain.Entity;
using TimeTracking.App.Phase.Domain.Entity;
using TimeTracking.App.Person.Domain.Entity;
using TimeTracking.App.Project.Domain.Entity;
using TimeTracking.App.Time.Domain.Entity;

namespace TimeTracking.App.Base
{
    public class TimeTrackingContext : DbContext
    {
        public TimeTrackingContext(DbContextOptions<TimeTrackingContext> options)
            : base(options)
        {
        }

        public DbSet<PersonEntity> Persons { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<PhaseEntity> Phases { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<TimeEntity> Times { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TimeEntity>()
                .HasOne<CategoryEntity>()
                .WithMany()
                .HasForeignKey(t => t.Category);

            modelBuilder.Entity<CategoryEntity>()
                .HasOne<PhaseEntity>()
                .WithMany()
                .HasForeignKey(c => c.Phase);

            modelBuilder.Entity<PhaseEntity>()
                .HasOne<ProjectEntity>()
                .WithMany()
                .HasForeignKey(p => p.Project);

            modelBuilder.Entity<ProjectEntity>()
                .HasOne<PersonEntity>()
                .WithMany()
                .HasForeignKey(p => p.UserCreated);
        }
    }
}
