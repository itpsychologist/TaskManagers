using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace TaskManagers.Data
{
    public class TaskManagersDbContextFactory : IDesignTimeDbContextFactory<TaskManagersDbContext>
    {
        public TaskManagersDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TaskManagersDbContext>();
            optionsBuilder.UseSqlite("Data Source=TaskManagers.db");

            return new TaskManagersDbContext(optionsBuilder.Options);
        }
    }
}
