using TaskManagers.Models;

namespace TaskManagers.Repositories
{
    public interface IWorkTaskRepository : IGenericRepository<WorkTask>
    {
        Task<IEnumerable<WorkTask>> GetTasksWithAssigneesAsync();
        Task<WorkTask> GetTaskWithDetailsAsync(int id);
    }
}
