using Microsoft.AspNetCore.Mvc;
using TaskManagers.Models;
using TaskManagers.Repositories;

namespace TaskManagers.Controllers
{
    public class PositionsController : Controller
    {
        private readonly IGenericRepository<Position> _repository;

        public PositionsController(IGenericRepository<Position> repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var positions = await _repository.GetAllAsync();
            return View(positions);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Position position)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(position);
                return RedirectToAction(nameof(Index));
            }
            return View(position);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var position = await _repository.GetByIdAsync(id);
            if (position == null) return NotFound();
            return View(position);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Position position)
        {
            if (id != position.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(position);
                return RedirectToAction(nameof(Index));
            }
            return View(position);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var position = await _repository.GetByIdAsync(id);
            if (position == null) return NotFound();
            await _repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
