using Microsoft.EntityFrameworkCore;
using TaskManagers.Models;

namespace TaskManagers.Data
{
    public class TaskManagersDbContext : DbContext
    {
        public TaskManagersDbContext(DbContextOptions<TaskManagersDbContext> options)
            : base(options)
        {
        }

        public DbSet<Position> Positions { get; set; }
        public DbSet<TaskType> TaskTypes { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<WorkTask> WorkTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Many-to-many relationship
            modelBuilder.Entity<WorkTask>()
                .HasMany(t => t.Assignees)
                .WithMany(w => w.AssignedTasks)
                .UsingEntity(j => j.ToTable("WorkerTasks"));

            // One-to-many: Worker and Position
            modelBuilder.Entity<Worker>()
                .HasOne(w => w.Position)
                .WithMany(p => p.Workers)
                .HasForeignKey(w => w.PositionId);

            // One-to-many: WorkTask and TaskType
            modelBuilder.Entity<WorkTask>()
                .HasOne(t => t.TaskType)
                .WithMany(tt => tt.Tasks)
                .HasForeignKey(t => t.TaskTypeId);
        }
    }
}
