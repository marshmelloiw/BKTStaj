using Microsoft.AspNetCore.Mvc;
using KampusTek.Business.Abstract;
using KampusTek.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KampusTekWebApp.Controllers
{
    public class RentalController : Controller
    {
        private readonly IRentalService _rentalService;
        private readonly IUserService _userService;
        private readonly IBicycleService _bicycleService;
        private readonly IStationService _stationService;

        public RentalController(IRentalService rentalService, IUserService userService, IBicycleService bicycleService, IStationService stationService)
        {
            _rentalService = rentalService;
            _userService = userService;
            _bicycleService = bicycleService;
            _stationService = stationService;
        }

        public IActionResult Index()
        {
            var rentals = _rentalService.GetAll();
            return View(rentals);
        }

        public IActionResult Details(int id)
        {
            var rental = _rentalService.GetById(id);
            if (rental == null)
                return NotFound();

            return View(rental);
        }

        public IActionResult Create()
        {
            var users = _userService.GetAll();
            var bicycles = _bicycleService.GetAll();
            var stations = _stationService.GetAll();

            ViewBag.Users = new SelectList(users.Select(u => new { Id = u.Id, Name = $"{u.FirstName} {u.LastName}" }), "Id", "Name");
            ViewBag.Bicycles = new SelectList(bicycles, "Id", "BicycleCode");
            ViewBag.Stations = new SelectList(stations, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Rental rental)
        {
            if (ModelState.IsValid)
            {
                _rentalService.Add(rental);
                return RedirectToAction(nameof(Index));
            }

            var users = _userService.GetAll();
            var bicycles = _bicycleService.GetAll();
            var stations = _stationService.GetAll();

            ViewBag.Users = new SelectList(users.Select(u => new { Id = u.Id, Name = $"{u.FirstName} {u.LastName}" }), "Id", "Name", rental.UserId);
            ViewBag.Bicycles = new SelectList(bicycles, "Id", "BicycleCode", rental.BicycleId);
            ViewBag.Stations = new SelectList(stations, "Id", "Name", rental.StartStationId);

            return View(rental);
        }

        public IActionResult Edit(int id)
        {
            var rental = _rentalService.GetById(id);
            if (rental == null)
                return NotFound();

            var users = _userService.GetAll();
            var bicycles = _bicycleService.GetAll();
            var stations = _stationService.GetAll();

            ViewBag.Users = new SelectList(users.Select(u => new { Id = u.Id, Name = $"{u.FirstName} {u.LastName}" }), "Id", "Name", rental.UserId);
            ViewBag.Bicycles = new SelectList(bicycles, "Id", "BicycleCode", rental.BicycleId);
            ViewBag.Stations = new SelectList(stations, "Id", "Name", rental.StartStationId);

            return View(rental);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Rental rental)
        {
            if (ModelState.IsValid)
            {
                _rentalService.Update(rental);
                return RedirectToAction(nameof(Index));
            }

            var users = _userService.GetAll();
            var bicycles = _bicycleService.GetAll();
            var stations = _stationService.GetAll();

            ViewBag.Users = new SelectList(users.Select(u => new { Id = u.Id, Name = $"{u.FirstName} {u.LastName}" }), "Id", "Name", rental.UserId);
            ViewBag.Bicycles = new SelectList(bicycles, "Id", "BicycleCode", rental.BicycleId);
            ViewBag.Stations = new SelectList(stations, "Id", "Name", rental.StartStationId);

            return View(rental);
        }

        public IActionResult Delete(int id)
        {
            var rental = _rentalService.GetById(id);
            if (rental == null)
                return NotFound();

            return View(rental);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _rentalService.Delete(id);
                TempData["SuccessMessage"] = "Kiralama kaydı başarıyla silindi.";
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Delete), new { id = id });
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Kiralama kaydı silinirken bir hata oluştu.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}