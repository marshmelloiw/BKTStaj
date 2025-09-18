using KampusTek.Business.Abstract;
using KampusTek.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KampusTekWebApp.Controllers
{
    public class BicycleController : Controller
    {
        private readonly IBicycleService _bicycleService;
        private readonly IStationService _stationService;

        public BicycleController(IBicycleService bicycleService, IStationService stationService)
        {
            _bicycleService = bicycleService;
            _stationService = stationService;
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
            var bicycle = new Bicycle
            {
                BicycleCode = _bicycleService.GetNextBicycleCode()
            };
            
            var stations = _stationService.GetAll();
            ViewBag.Stations = new SelectList(stations, "Id", "Name");
            
            return View(bicycle);
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
            
            var stations = _stationService.GetAll();
            ViewBag.Stations = new SelectList(stations, "Id", "Name", bicycle.CurrentStationId);
            return View(bicycle);
        }

        public IActionResult Edit(int id)
        {
            var bicycle = _bicycleService.GetById(id);
            if (bicycle == null)
                return NotFound();

            var stations = _stationService.GetAll();
            ViewBag.Stations = new SelectList(stations, "Id", "Name", bicycle.CurrentStationId);
            
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
            
            var stations = _stationService.GetAll();
            ViewBag.Stations = new SelectList(stations, "Id", "Name", bicycle.CurrentStationId);
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
            try
            {
                _bicycleService.Delete(id);
                TempData["SuccessMessage"] = "Bisiklet başarıyla silindi.";
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Delete), new { id = id });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Bisiklet silinirken bir hata oluştu.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
