using DataAccessLayer2.Context;
using EntityLayer22.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Ticaret.Controllers
{
    public class CartController : Controller
    {
        private readonly DataContext _context;

        public CartController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // 1) إذا لم يكن المستخدم مسجَّل دخولاً نعيده للـ Login
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            // 2) جلب بيانات المستخدم من الـ Claim
            var username = User.Identity.Name;
            var user = _context.Users
                       .FirstOrDefault(u => u.Email == username);
            if (user == null)
                return RedirectToAction("Login", "Account");

            // 3) جلب العناصر مع الـ Product عبر Include
            var cartItems = _context.Carts
                             .Include(c => c.Product)
                             .Where(c => c.UserId == user.Id)
                             .ToList();

            // 4) حساب المجموع
            if (!cartItems.Any())
            {
                ViewBag.Tutar = "Sepetinizde ürün bulunmamaktadır.";
            }
            else
            {
                // إمّا إذا خزنّت السعر في Cart.Price:
                var total = cartItems.Sum(c => c.Price);

                // أو تحسبي مباشرة:
                // var total = cartItems.Sum(c => c.Product.Price * c.Quantity);

                ViewBag.Tutar = $"Toplam Tutar = {total:N2} TL";
            }

            // 5) إرجاع الـ View مع الموديل
            return View(cartItems);
        }

        public IActionResult AddCart(int id)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            //‑‑ 1) المستخدم
            var user = _context.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
            if (user == null) return NotFound();

            //‑‑ 2) المنتج المطلوب  (السطر المفقود!)
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            //‑‑ 3) هل العنصر موجود مسبقاً في السلة؟
            var existing = _context.Carts
                                   .FirstOrDefault(c => c.UserId == user.Id && c.ProductId == id);

            if (existing != null)
            {
                existing.Quantity++;
                existing.Price = existing.Quantity * product.Price;
                existing.Image = product.Image;
                _context.Carts.Update(existing);
            }
            else
            {
                var cartItem = new Cart
                {
                    UserId = user.Id,
                    ProductId = product.Id,
                    Quantity = 1,
                    Price = product.Price,
                    Image = product.Image,
                    Date = DateTime.Now
                };
                _context.Carts.Add(cartItem);
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Cart");
        }




        public ActionResult TotalCount(int? count)
        {
            if (User.Identity.IsAuthenticated)
            {
                var model = _context.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
                count = _context.Carts.Where(x => x.UserId == model.Id).Count();
                ViewBag.count = count;
                if (count == 0)
                {
                    ViewBag.count = "";
                }
            }
            return PartialView();
            //actionresult belirtip partialgönderdiğin zaman diğer sayfadan karşılarken action değil renderaction yazılmalı
        }

        public void DinamikMiktar(int id, int miktari)
        {
            var model = _context.Carts.Find(id);
            model.Quantity = miktari;
            model.Price = model.Price * model.Quantity;
            _context.SaveChanges();
        }

        public ActionResult azalt(int id)
        {
            var model = _context.Carts.Find(id);
            if (model.Quantity == 1)
            {
                _context.Carts.Remove(model);
                _context.SaveChanges();
            }
            model.Quantity--;
            model.Price = model.Price * model.Quantity;
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult arttir(int id)
        {
            var model = _context.Carts.Find(id);
            model.Quantity++;
            model.Price = model.Price * model.Quantity;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var sil = _context.Carts.Find(id);
            _context.Carts.Remove(sil);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteRange()
        {
            if (User.Identity.IsAuthenticated)
            {
                var kullaniciEmail = User.Identity.Name;
                var user = _context.Users.FirstOrDefault(x => x.Email == kullaniciEmail);
                if (user == null)
                    return NotFound();   // لو لم يُعثر على المستخدم

                var items = _context.Carts.Where(x => x.UserId == user.Id);
                _context.Carts.RemoveRange(items);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // لو لم يكن مسجلاً دخولاً
            return RedirectToAction("Login", "Account");

        }

    }
}
