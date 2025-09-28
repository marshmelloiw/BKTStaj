using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using KampusTek.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace KampusTekWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly KampusTekDbContext _dbContext;

        public AccountController(KampusTekDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            ViewBag.UserTypes = await _dbContext.UserTypes.ToListAsync();
            return View();
        }

        public class RegisterViewModel
        {
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string CellNumber { get; set; } = string.Empty;
            public int UserTypeId { get; set; }
            public string Password { get; set; } = string.Empty;
            public string ConfirmPassword { get; set; } = string.Empty;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.UserTypes = await _dbContext.UserTypes.ToListAsync();
                return View(model);
            }

            if (string.IsNullOrWhiteSpace(model.FirstName) ||
                string.IsNullOrWhiteSpace(model.LastName) ||
                string.IsNullOrWhiteSpace(model.Email) ||
                string.IsNullOrWhiteSpace(model.CellNumber) ||
                string.IsNullOrWhiteSpace(model.Password) ||
                model.UserTypeId <= 0)
            {
                ModelState.AddModelError(string.Empty, "Tüm alanlar zorunludur.");
                ViewBag.UserTypes = await _dbContext.UserTypes.ToListAsync();
                return View(model);
            }

            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Şifreler eşleşmiyor.");
                ViewBag.UserTypes = await _dbContext.UserTypes.ToListAsync();
                return View(model);
            }

            var emailExists = await _dbContext.Users.AnyAsync(u => u.Email == model.Email);
            if (emailExists)
            {
                ModelState.AddModelError(string.Empty, "Bu e-posta zaten kayıtlı.");
                ViewBag.UserTypes = await _dbContext.UserTypes.ToListAsync();
                return View(model);
            }

            var userTypeExists = await _dbContext.UserTypes.AnyAsync(ut => ut.Id == model.UserTypeId);
            if (!userTypeExists)
            {
                ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı tipi.");
                ViewBag.UserTypes = await _dbContext.UserTypes.ToListAsync();
                return View(model);
            }

            CreatePasswordHash(model.Password, out var passwordHash, out var passwordSalt);

            var user = new KampusTek.Entities.User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                CellNumber = model.CellNumber,
                UserTypeId = model.UserTypeId,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            // Don't auto-login; send user to login page after successful registration
            TempData["RegisterSuccess"] = "Kayıt başarıyla tamamlandı. Lütfen giriş yapın.";
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public class LoginViewModel
        {
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public string? ReturnUrl { get; set; }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _dbContext.Users.Include(u => u.UserType).FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null || user.PasswordHash == null || user.PasswordSalt == null)
            {
                ModelState.AddModelError(string.Empty, "Geçersiz e-posta veya şifre.");
                return View(model);
            }

            if (!VerifyPassword(model.Password, user.PasswordHash, user.PasswordSalt))
            {
                ModelState.AddModelError(string.Empty, "Geçersiz e-posta veya şifre.");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserType?.Name ?? user.UserTypeId.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
            {
                IsPersistent = true
            });

            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                return Redirect(model.ReturnUrl);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        private static bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            using var hmac = new HMACSHA512(storedSalt);
            var computed = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computed.SequenceEqual(storedHash);
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

    }
}


