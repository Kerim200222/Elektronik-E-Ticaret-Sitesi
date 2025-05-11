using DataAccessLayer2.Context;
using EntityLayer22.Entities;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc;
using PagedList.Mvc;
using PagedList;
using X.PagedList;
using Microsoft.EntityFrameworkCore;

namespace E_Ticaret.Controllers
{
    public class SalesController : Controller
    {
        private readonly DataContext _db;

        // 1) Constructor Injection
        public SalesController(DataContext db)
        {
            _db = db;
        }

        public IActionResult Index(int sayfa = 1)
        {
            // 2) إذا لم يكن المستخدم مسجَّل دخولًا، نعيده للـ Login
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            // 3) جلب بيانات المستخدم
            var email = User.Identity.Name;
            var user = _db.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
                return RedirectToAction("Login", "Account");

            // 4) جلب قائمة المبيعات مع الـ paging
            //   استخدمي X.PagedList.Mvc/Core:
            var sales = _db.Sales
                           .Include(s=>s.Product)
                           .Where(s => s.UserId == user.Id)
                           .OrderByDescending(s => s.Date)
                           .ToPagedList(sayfa, 5);

            return View(sales);
        }

        // GET: /Sales/Buy/5
        public IActionResult Buy(int id)
        {
            // نستخدم Include حتى يحمل لنا الـ Product
            var model = _db.Carts
                           .Include(c => c.Product)
                           .FirstOrDefault(c => c.Id == id);

            if (model == null)
            {
                // لو ما فيه عنصر، نرجّعه لقائمة السلة أو 404
                return RedirectToAction("Index", "Cart");
                // أو: return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Buy2(int id)
        {
            // 1) جلب عنصر السلة مع المنتج
            var cart = _db.Carts
                          .Include(c => c.Product)
                          .FirstOrDefault(c => c.Id == id);
            if (cart == null)
                return RedirectToAction("Index", "Cart");

            // 2) أنشئ سجل البيع
            var sale = new Sales
            {
                UserId = cart.UserId,
                ProductId = cart.ProductId,
                Quantity = cart.Quantity,
                Image = cart.Product.Image,
                Price = cart.Price,
                Date = DateTime.Now
            };

            // 3) خفّض الكمية من جدول المنتجات
            cart.Product.Stock -= cart.Quantity;
            _db.Products.Update(cart.Product);

            // 4) إحذف من السلة وأضف للبيع
            _db.Carts.Remove(cart);
            _db.Sales.Add(sale);

            // 5) احفظ التغييرات دفعة واحدة
            _db.SaveChanges();

            ViewBag.islem = "Satın alma işlemi başarılı bir şekilde gerçekleşmiştir";
            return View("islem");
        }

        public IActionResult BuyAll()
        {
            if (!User.Identity.IsAuthenticated)
            {
                // إذا لم يكن مسجّل دخولاً نعيده للـ Login
                return RedirectToAction("Login", "Account");
            }

            var email = User.Identity.Name;
            var user = _db.Users.FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                // إذا ما وجدنا المستخدم، نرجّع 404
                return NotFound();
            }

            var items = _db.Carts
                           .Include(c => c.Product)
                           .Where(c => c.UserId == user.Id)
                           .ToList();

            if (!items.Any())
            {
                ViewBag.Tutar = "Sepetinize ürün bulunmamaktadır.";
            }
            else
            {
                var toplam = items.Sum(c => c.Product.Price * c.Quantity);
                ViewBag.Tutar = $"Toplam Tutar = {toplam:N2} TL";
            }

            return View(items);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BuyAll2()
        {
            // 1) إذا لم يكن مسجّل دخولاً نعيده لصفحة تسجيل الدخول
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            // 2) جلب المستخدم من قاعدة البيانات
            var email = User.Identity.Name;
            var user = _db.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
                return NotFound();

            // 3) جلب عناصر السلة الخاصة به (مع إحالة الـ Product إن أردت)
            var cartItems = _db.Carts
                .Where(c => c.UserId == user.Id)
                .ToList();

            // 4) إذا كانت السلة فارغة، نعيد رسالة أو نوجّهه لصفحة السلة
            if (!cartItems.Any())
            {
                TempData["Error"] = "Sepetinizde ürün bulunmamaktadır.";
                return RedirectToAction("Index", "Cart");
            }

            // 5) تحويل كل عنصر في السلة إلى سجل بيع
            var salesEntries = cartItems.Select(c => new Sales
            {
                UserId = c.UserId,
                ProductId = c.ProductId,
                Quantity = c.Quantity,
                Price = c.Price,
                Image = c.Image,
                Date = DateTime.Now
            }).ToList();

            // 6) إضافة جميع عمليات البيع دفعة واحدة
            _db.Sales.AddRange(salesEntries);

            // 7) حذف جميع عناصر السلة
            _db.Carts.RemoveRange(cartItems);

            // 8) حفظ التغييرات مرة واحدة
            _db.SaveChanges();

            // 9) إعادة التوجيه إلى صفحة السلة أو صفحة تأكيد العملية
            TempData["Success"] = "Satın alma işlemi başarılı bir şekilde gerçekleşmiştir.";
            return RedirectToAction("Index", "Cart");
        }


    }
}
