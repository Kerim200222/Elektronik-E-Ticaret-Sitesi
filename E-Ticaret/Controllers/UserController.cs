using Microsoft.AspNetCore.Mvc;
using DataAccessLayer2.Context;
using EntityLayer22.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;

namespace E_Ticaret.Controllers
{
    public class UserController : Controller
    {
        private readonly DataContext _db;
        private readonly EmailSettings _email;

        public UserController(DataContext db, IOptions<EmailSettings> emailOptions)
        {
            _db = db;
            _email = emailOptions.Value;
        }


        // GET: /User/Update
        [HttpGet]                        
        public IActionResult Update()
        {
            // لا حاجة لـ Session … نأخذ البريد من الـ Claims
            string email = User.Identity?.Name;
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login", "Account");

            var user = _db.Users.FirstOrDefault(u => u.Email == email);
            if (user == null) return NotFound();

            return View(user);          // يبحث عن Views/User/Update.cshtml
        }

        // POST: /User/Update
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Update(User data)
        {
            

            string email = User.Identity?.Name;
            var user = _db.Users.FirstOrDefault(u => u.Email == email);
            if (user == null) return NotFound();

            user.Name = data.Name;
            user.SurName = data.SurName;
            user.UserName = data.UserName;
            user.Email = data.Email;
            user.Password = data.Password;   

            _db.Update(user);
            _db.SaveChanges();

            TempData["Success"] = "Bilgileriniz güncellendi";
            return RedirectToAction("Login", "Account");
        }

        public ActionResult PasswordReset()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PasswordReset(string eposta)
        {
            var user = _db.Users.SingleOrDefault(u => u.Email == eposta);
            if (user == null)
            {
                ViewBag.uyari = "E‑posta bulunamadı";
                return View();
            }

            // 1) إنشاء كلمة سر عشوائية (6 أرقام مثلاً)
            var rnd = new Random();
            var newPass = rnd.Next(100000, 999999).ToString();

            // يجب تجزئة كلمة السر وتخزين الـ hash فقط
            user.Password = newPass; 
            _db.SaveChanges();

            // 2) إرسال الرسالة
            try
            {
                using var smtp = new SmtpClient
                {
                    Host = _email.Host,
                    Port = _email.Port,
                    EnableSsl = _email.EnableSSL,
                    Credentials = new NetworkCredential(_email.User, _email.Password)
                };

                var message = new MailMessage
                {
                    From = new MailAddress(_email.User, "E‑Ticaret"),
                    Subject = "Yeni Şifreniz",
                    Body = $"Merhaba {user.Name},\nYeni şifreniz: {newPass}",
                    IsBodyHtml = false
                };
                message.To.Add(eposta);

                smtp.Send(message);
                ViewBag.uyari = "Şifreniz e‑posta adresinize gönderildi.";
            }
            catch (Exception ex)
            {
                // سجّل الخطأ ex
                ViewBag.uyari = "E‑posta gönderilemedi. Lütfen tekrar deneyin.";
            }

            return View();
        }


    }
}