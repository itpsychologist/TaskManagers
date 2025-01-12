using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using TaskManagers.Models;
using TaskManagers.Repositories;

namespace TaskManagers.Controllers
{
    public class WorkersController : Controller
    {
        private readonly IGenericRepository<Worker> _workerRepository;
        private readonly IGenericRepository<Position> _positionRepository;

        public WorkersController(
            IGenericRepository<Worker> workerRepository,
            IGenericRepository<Position> positionRepository)
        {
            _workerRepository = workerRepository;
            _positionRepository = positionRepository;
        }

        public async Task<IActionResult> Index()
        {
            var workers = await _workerRepository.GetAllAsync();
            return View(workers);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Positions = new SelectList(await _positionRepository.GetAllAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Worker worker)
        {
            if (ModelState.IsValid)
            {
                await _workerRepository.AddAsync(worker);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Positions = new SelectList(await _positionRepository.GetAllAsync(), "Id", "Name");
            return View(worker);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var worker = await _workerRepository.GetByIdAsync(id);
            if (worker == null) return NotFound();

            ViewBag.Positions = new SelectList(await _positionRepository.GetAllAsync(), "Id", "Name");
            return View(worker);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Worker worker)
        {
            if (id != worker.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _workerRepository.UpdateAsync(worker);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Positions = new SelectList(await _positionRepository.GetAllAsync(), "Id", "Name");
            return View(worker);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var worker = await _workerRepository.GetByIdAsync(id);
            if (worker == null) return NotFound();
            await _workerRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
