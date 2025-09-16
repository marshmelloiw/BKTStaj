using KampusTek.Business.Abstract;
using KampusTek.Entities;
using Microsoft.AspNetCore.Mvc;

namespace KampusTekWebApp.Controllers
{
    public class BicycleController : Controller
    {
        private readonly IBicycleService _bicycleService;

        public BicycleController(IBicycleService bicycleService)
        {
            _bicycleService = bicycleService;
        }

        public IActionResult Index()
        {
            var bicycles = _bicycleService.GetAll();
            return View(bicycles);
        }

        public IActionResult Details(int id)
        {
            var bicycle = _bicycleService.GetById(id);
            if (bicycle == null)
                return NotFound();

            return View(bicycle);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Bicycle bicycle)
        {
            if (ModelState.IsValid)
            {
                _bicycleService.Add(bicycle);
                return RedirectToAction(nameof(Index));
            }
            return View(bicycle);
        }

        public IActionResult Edit(int id)
        {
            var bicycle = _bicycleService.GetById(id);
            if (bicycle == null)
                return NotFound();

            return View(bicycle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Bicycle bicycle)
        {
            if (ModelState.IsValid)
            {
                _bicycleService.Update(bicycle);
                return RedirectToAction(nameof(Index));
            }
            return View(bicycle);
        }

        public IActionResult Delete(int id)
        {
            var bicycle = _bicycleService.GetById(id);
            if (bicycle == null)
                return NotFound();

            return View(bicycle);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _bicycleService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
