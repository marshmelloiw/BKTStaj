using Microsoft.AspNetCore.Mvc;
using KampusTek.Business.Abstract;

namespace KampusTekWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IBicycleService _bicycleService;
        private readonly IRentalService _rentalService;
        private readonly IStationService _stationService;

        public HomeController(IUserService userService, IBicycleService bicycleService, IRentalService rentalService, IStationService stationService)
        {
            _userService = userService;
            _bicycleService = bicycleService;
            _rentalService = rentalService;
            _stationService = stationService;
        }

        public IActionResult Index()
        {
            ViewBag.UserCount = _userService.Count();
            ViewBag.BicycleCount = _bicycleService.Count();
            ViewBag.ActiveRentals = _rentalService.Count(r => r.ReturnTime == null);
            ViewBag.StationCount = _stationService.Count();
            return View();
        }
    }
}
