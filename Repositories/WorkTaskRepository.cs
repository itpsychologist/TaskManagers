using Microsoft.EntityFrameworkCore;
using TaskManagers.Data;
using TaskManagers.Models;

namespace TaskManagers.Repositories
{
    public class WorkTaskRepository : GenericRepository<WorkTask>, IWorkTaskRepository
    {
        public WorkTaskRepository(TaskManagersDbContext context) : base(context) { }

        public async Task<IEnumerable<WorkTask>> GetTasksWithAssigneesAsync()
        {
            return await _context.WorkTasks
                .Include(t => t.TaskType)
                .Include(t => t.Assignees)
                    .ThenInclude(w => w.Position)
                .ToListAsync();
        }

        public async Task<WorkTask> GetTaskWithDetailsAsync(int id)
        {
            return await _context.WorkTasks
                .Include(t => t.TaskType)
                .Include(t => t.Assignees)
                    .ThenInclude(w => w.Position)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
