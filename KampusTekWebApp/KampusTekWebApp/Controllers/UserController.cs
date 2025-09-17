using KampusTek.Business.Abstract;
using KampusTek.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KampusTekWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserTypeService _userTypeService;

        public UserController(IUserService userService, IUserTypeService userTypeService)
        {
            _userService = userService;
            _userTypeService = userTypeService;
        }

        public IActionResult Index()
        {
            var users = _userService.GetAll();
            return View(users);
        }

        public IActionResult Details(int id)
        {
            var user = _userService.GetById(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        public IActionResult Create()
        {
            var userTypes = _userTypeService.GetAll();
            ViewBag.UserTypes = new SelectList(userTypes, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _userService.Add(user);
                return RedirectToAction(nameof(Index));
            }

            var userTypes = _userTypeService.GetAll();
            ViewBag.UserTypes = new SelectList(userTypes, "Id", "Name", user.UserTypeId);
            return View(user);
        }

        public IActionResult Edit(int id)
        {
            var user = _userService.GetById(id);
            if (user == null)
                return NotFound();

            var userTypes = _userTypeService.GetAll();
            ViewBag.UserTypes = new SelectList(userTypes, "Id", "Name", user.UserTypeId);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                _userService.Update(user);
                return RedirectToAction(nameof(Index));
            }

            var userTypes = _userTypeService.GetAll();
            ViewBag.UserTypes = new SelectList(userTypes, "Id", "Name", user.UserTypeId);
            return View(user);
        }

        public IActionResult Delete(int id)
        {
            var user = _userService.GetById(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _userService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
