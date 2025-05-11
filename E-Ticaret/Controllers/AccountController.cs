using System.Security.Claims;
using DataAccessLayer2.Context;
using EntityLayer22.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(DataContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User data)
        {
            var user = _db.Users
                .FirstOrDefault(x => x.Email == data.Email && x.Password == data.Password);

            if (user == null)
            {
                ViewBag.hata = "Mail veya şifreniz hatalı";
                return View(data);
            }

            // 1) جهّز Claims
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Email),
        new Claim("FullName", $"{user.Name} {user.SurName}"),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),

        // ← هذا السطر يضيف دور المستخدم حقًا
        new Claim(ClaimTypes.Role, user.Role ?? "User")
    };

            var identity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // 2) سجّل الدخول بالكوكي
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties { IsPersistent = false });

            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
            => View();

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User data)
        {
            data.Role = "User";
            if (data.Password != data.RePassword)
                ModelState.AddModelError("RePassword", "Şifreler eşleşmiyor");

            if (!ModelState.IsValid)
                return View(data);

            _db.Users.Add(data);
            await _db.SaveChangesAsync();
            return RedirectToAction("Login");
        }



    }
}
