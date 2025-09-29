using Microsoft.AspNetCore.Mvc;
using KampusTek.Business.Abstract;
using KampusTek.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace KampusTekWebApp.Controllers
{
    [Authorize]
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
            var isAdmin = User.IsInRole("Admin");
            int currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var rentals = isAdmin
                ? _rentalService.GetAll()
                : _rentalService.GetAll(r => r.UserId == currentUserId);

            ViewBag.CurrentUserId = currentUserId;
            ViewBag.IsAdmin = isAdmin;
            return View(rentals);
        }

        public IActionResult Details(int id)
        {
            var rental = _rentalService.GetById(id);
            if (rental == null)
                return NotFound();
            var isAdmin = User.IsInRole("Admin");
            int currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (!isAdmin && rental.UserId != currentUserId)
                return Forbid();

            return View(rental);
        }

        public IActionResult Create()
        {
            var isAdmin = User.IsInRole("Admin");
            var users = isAdmin ? _userService.GetAll() : _userService.GetAll(u => u.Id == int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            // Only available bicycles (not currently rented)
            var bicycles = _bicycleService.GetAll()
                .Where(b => !_rentalService.Exists(r => r.BicycleId == b.Id && r.ReturnTime == null))
                .ToList();
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
                try
                {
                    var isAdmin = User.IsInRole("Admin");
                    if (!isAdmin)
                    {
                        // force current user as renter
                        rental.UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    }
                    _rentalService.Add(rental);
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            var isAdmin2 = User.IsInRole("Admin");
            var users = isAdmin2 ? _userService.GetAll() : _userService.GetAll(u => u.Id == int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            var bicycles = _bicycleService.GetAll()
                .Where(b => !_rentalService.Exists(r => r.BicycleId == b.Id && r.ReturnTime == null) || b.Id == rental.BicycleId)
                .ToList();
            var stations = _stationService.GetAll();

            ViewBag.Users = new SelectList(users.Select(u => new { Id = u.Id, Name = $"{u.FirstName} {u.LastName}" }), "Id", "Name", rental.UserId);
            ViewBag.Bicycles = new SelectList(bicycles, "Id", "BicycleCode", rental.BicycleId);
            ViewBag.Stations = new SelectList(stations, "Id", "Name", rental.StartStationId);

            return View(rental);
        }

        // GET: end rental page
        public IActionResult End(int id)
        {
            var rental = _rentalService.GetById(id);
            if (rental == null)
                return NotFound();

            var isAdmin = User.IsInRole("Admin");
            int currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (!isAdmin && rental.UserId != currentUserId)
                return Forbid();

            if (rental.ReturnTime != null)
            {
                TempData["ErrorMessage"] = "Bu kiralama zaten tamamlanmış.";
                return RedirectToAction(nameof(Index));
            }

            var stations = _stationService.GetAll();
            ViewBag.Stations = new SelectList(stations, "Id", "Name");
            return View(rental);
        }

        // POST: complete rental
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult End(int id, int endStationId)
        {
            var rental = _rentalService.GetById(id);
            if (rental == null)
                return NotFound();

            var isAdmin = User.IsInRole("Admin");
            int currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (!isAdmin && rental.UserId != currentUserId)
                return Forbid();

            if (rental.ReturnTime != null)
            {
                TempData["ErrorMessage"] = "Bu kiralama zaten tamamlanmış.";
                return RedirectToAction(nameof(Index));
            }

            _rentalService.EndRental(id, endStationId);
            TempData["SuccessMessage"] = "Kiralama başarıyla tamamlandı.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            if (!User.IsInRole("Admin"))
                return Forbid();
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
            if (!User.IsInRole("Admin"))
                return Forbid();
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
            if (!User.IsInRole("Admin"))
                return Forbid();
            var rental = _rentalService.GetById(id);
            if (rental == null)
                return NotFound();

            return View(rental);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!User.IsInRole("Admin"))
                return Forbid();
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