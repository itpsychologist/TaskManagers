using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagers.Models;
using TaskManagers.Repositories;

namespace TaskManagers.Controllers
{
    public class WorkTasksController : Controller
    {
        private readonly IWorkTaskRepository _taskRepository;
        private readonly IGenericRepository<TaskType> _taskTypeRepository;
        private readonly IGenericRepository<Worker> _workerRepository;

        public WorkTasksController(
            IWorkTaskRepository taskRepository,
            IGenericRepository<TaskType> taskTypeRepository,
            IGenericRepository<Worker> workerRepository)
        {
            _taskRepository = taskRepository;
            _taskTypeRepository = taskTypeRepository;
            _workerRepository = workerRepository;
        }

        public async Task<IActionResult> Index()
        {
            var tasks = await _taskRepository.GetTasksWithAssigneesAsync();
            return View(tasks);
        }

        public async Task<IActionResult> Details(int id)
        {
            var task = await _taskRepository.GetTaskWithDetailsAsync(id);
            if (task == null) return NotFound();
            return View(task);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.TaskTypes = new SelectList(await _taskTypeRepository.GetAllAsync(), "Id", "Name");
            ViewBag.Workers = new SelectList(await _workerRepository.GetAllAsync(), "Id", "Username");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WorkTask task, int[] selectedWorkers)
        {
            if (ModelState.IsValid)
            {
                task.Assignees = new List<Worker>();
                foreach (var workerId in selectedWorkers)
                {
                    var worker = await _workerRepository.GetByIdAsync(workerId);
                    task.Assignees.Add(worker);
                }
                await _taskRepository.AddAsync(task);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.TaskTypes = new SelectList(await _taskTypeRepository.GetAllAsync(), "Id", "Name");
            ViewBag.Workers = new SelectList(await _workerRepository.GetAllAsync(), "Id", "Username");
            return View(task);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var task = await _taskRepository.GetTaskWithDetailsAsync(id);
            if (task == null) return NotFound();

            ViewBag.TaskTypes = new SelectList(await _taskTypeRepository.GetAllAsync(), "Id", "Name");
            ViewBag.Workers = new SelectList(await _workerRepository.GetAllAsync(), "Id", "Username");
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WorkTask task, int[] selectedWorkers)
        {
            if (id != task.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existingTask = await _taskRepository.GetTaskWithDetailsAsync(id);
                    existingTask.Name = task.Name;
                    existingTask.Description = task.Description;
                    existingTask.Deadline = task.Deadline;
                    existingTask.IsCompleted = task.IsCompleted;
                    existingTask.Priority = task.Priority;
                    existingTask.TaskTypeId = task.TaskTypeId;

                    existingTask.Assignees.Clear();
                    foreach (var workerId in selectedWorkers)
                    {
                        var worker = await _workerRepository.GetByIdAsync(workerId);
                        existingTask.Assignees.Add(worker);
                    }

                    await _taskRepository.UpdateAsync(existingTask);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await TaskExists(id))
                        return NotFound();
                    throw;
                }
            }
            ViewBag.TaskTypes = new SelectList(await _taskTypeRepository.GetAllAsync(), "Id", "Name");
            ViewBag.Workers = new SelectList(await _workerRepository.GetAllAsync(), "Id", "Username");
            return View(task);
        }

        private async Task<bool> TaskExists(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            return task != null;
        }
    }
}
