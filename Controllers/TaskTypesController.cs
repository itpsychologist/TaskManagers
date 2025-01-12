using Microsoft.AspNetCore.Mvc;
using TaskManagers.Models;
using TaskManagers.Repositories;

namespace TaskManagers.Controllers
{
    public class TaskTypesController : Controller
    {
        private readonly IGenericRepository<TaskType> _repository;

        public TaskTypesController(IGenericRepository<TaskType> repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var taskTypes = await _repository.GetAllAsync();
            return View(taskTypes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskType taskType)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(taskType);
                return RedirectToAction(nameof(Index));
            }
            return View(taskType);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var taskType = await _repository.GetByIdAsync(id);
            if (taskType == null) return NotFound();
            return View(taskType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TaskType taskType)
        {
            if (id != taskType.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(taskType);
                return RedirectToAction(nameof(Index));
            }
            return View(taskType);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var taskType = await _repository.GetByIdAsync(id);
            if (taskType == null) return NotFound();
            await _repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
