using Microsoft.AspNetCore.Mvc;
using KampusTek.Business.Abstract;
using KampusTek.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KampusTekWebApp.Controllers
{
    public class MaintenanceController : Controller
    {
        private readonly IMaintenanceService _maintenanceService;
        private readonly IBicycleService _bicycleService;

        public MaintenanceController(IMaintenanceService maintenanceService, IBicycleService bicycleService)
        {
            _maintenanceService = maintenanceService;
            _bicycleService = bicycleService;
        }

        public IActionResult Index()
        {
            var maintenances = _maintenanceService.GetAll();
            return View(maintenances);
        }

        public IActionResult Details(int id)
        {
            var maintenance = _maintenanceService.GetById(id);
            if (maintenance == null)
                return NotFound();

            return View(maintenance);
        }

        public IActionResult Create()
        {
            var bicycles = _bicycleService.GetAll();
            ViewBag.Bicycles = new SelectList(bicycles, "Id", "BicycleCode");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Maintenance maintenance)
        {
            if (ModelState.IsValid)
            {
                _maintenanceService.Add(maintenance);
                return RedirectToAction(nameof(Index));
            }

            var bicycles = _bicycleService.GetAll();
            ViewBag.Bicycles = new SelectList(bicycles, "Id", "BicycleCode", maintenance.BicycleId);
            return View(maintenance);
        }

        public IActionResult Edit(int id)
        {
            var maintenance = _maintenanceService.GetById(id);
            if (maintenance == null)
                return NotFound();

            var bicycles = _bicycleService.GetAll();
            ViewBag.Bicycles = new SelectList(bicycles, "Id", "BicycleCode", maintenance.BicycleId);
            return View(maintenance);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Maintenance maintenance)
        {
            if (ModelState.IsValid)
            {
                _maintenanceService.Update(maintenance);
                return RedirectToAction(nameof(Index));
            }

            var bicycles = _bicycleService.GetAll();
            ViewBag.Bicycles = new SelectList(bicycles, "Id", "BicycleCode", maintenance.BicycleId);
            return View(maintenance);
        }

        public IActionResult Delete(int id)
        {
            var maintenance = _maintenanceService.GetById(id);
            if (maintenance == null)
                return NotFound();

            return View(maintenance);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _maintenanceService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
