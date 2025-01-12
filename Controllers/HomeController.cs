using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TaskManagers.Models;
using TaskManagers.Repositories;


namespace TaskManagers.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWorkTaskRepository _taskRepository;
        private readonly IGenericRepository<Worker> _workerRepository;
        private readonly IGenericRepository<TaskType> _taskTypeRepository;

        public HomeController(
            IWorkTaskRepository taskRepository,
            IGenericRepository<Worker> workerRepository,
            IGenericRepository<TaskType> taskTypeRepository)
        {
            _taskRepository = taskRepository;
            _workerRepository = workerRepository;
            _taskTypeRepository = taskTypeRepository;
        }

        public async Task<IActionResult> Index()
        {
            var tasks = await _taskRepository.GetTasksWithAssigneesAsync();
            var workers = await _workerRepository.GetAllAsync();

            var viewModel = new DashboardViewModel
            {
                TotalTasks = tasks.Count(),
                CompletedTasks = tasks.Count(t => t.IsCompleted),
                UrgentTasks = tasks.Count(t => t.Priority == Priority.Urgent && !t.IsCompleted),
                TotalWorkers = workers.Count(),
                RecentTasks = tasks.OrderByDescending(t => t.Deadline).Take(5).ToList(),
                TasksByType = tasks.GroupBy(t => t.TaskType.Name)
                                  .ToDictionary(g => g.Key, g => g.Count()),
                TasksByPriority = tasks.GroupBy(t => t.Priority.ToString())
                                     .ToDictionary(g => g.Key, g => g.Count())
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
