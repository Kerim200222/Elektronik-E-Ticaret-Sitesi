using Microsoft.AspNetCore.Mvc;
using DataAccessLayer2.Context;

namespace E_Ticaret.Controllers
{
    public class AdminIstatisticController : Controller
    {
        private readonly DataContext _db;
        public AdminIstatisticController(DataContext db) {  _db = db; }
        public ActionResult Index()
        {
            var satis = _db.Sales.Count();
            ViewBag.satis = satis;
            var urun = _db.Products.Count();
            ViewBag.urun = urun;
            var kategori = _db.Categorys.Count();
            ViewBag.kategori = kategori;
            var sepet = _db.Carts.Count();
            ViewBag.sepet = sepet;
            var user = _db.Users.Count();
            ViewBag.kullanici = user;

            return View();
        }


    }
}
