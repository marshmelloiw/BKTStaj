using Microsoft.AspNetCore.Mvc;
using KampusTek.Business.Abstract;
using KampusTek.Entities;

namespace KampusTekWebApp.Controllers
{
    public class StationController : Controller
    {
        private readonly IStationService _stationService;

        public StationController(IStationService stationService)
        {
            _stationService = stationService;
        }

        public IActionResult Index()
        {
            var stations = _stationService.GetAll();
            return View(stations);
        }

        public IActionResult Details(int id)
        {
            var station = _stationService.GetById(id);
            if (station == null)
                return NotFound();

            return View(station);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Station station)
        {
            if (ModelState.IsValid)
            {
                _stationService.Add(station);
                return RedirectToAction(nameof(Index));
            }
            return View(station);
        }

        public IActionResult Edit(int id)
        {
            var station = _stationService.GetById(id);
            if (station == null)
                return NotFound();

            return View(station);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Station station)
        {
            if (ModelState.IsValid)
            {
                _stationService.Update(station);
                return RedirectToAction(nameof(Index));
            }
            return View(station);
        }

        public IActionResult Delete(int id)
        {
            var station = _stationService.GetById(id);
            if (station == null)
                return NotFound();

            return View(station);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _stationService.Delete(id);
                TempData["SuccessMessage"] = "İstasyon başarıyla silindi.";
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Delete), new { id = id });
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "İstasyon silinirken bir hata oluştu.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}