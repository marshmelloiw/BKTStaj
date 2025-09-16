using Microsoft.AspNetCore.Mvc;
using KampusTek.Business.Abstract;
using KampusTek.Entities;

namespace KampusTekWebApp.Controllers
{
    public class RentalController : Controller
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
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
            return View(rental);
        }

        public IActionResult Edit(int id)
        {
            var rental = _rentalService.GetById(id);
            if (rental == null)
                return NotFound();

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
            _rentalService.Update(new Rental { Id = id, ReturnTime = DateTime.Now });
            return RedirectToAction(nameof(Index));
        }
    }
}