using BusinessLayer2.Concrete;
using DataAccessLayer2.Context;
using EntityLayer22.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;
using X.PagedList.Extensions;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;      
using PagedList.Mvc;


namespace E_Ticaret.Controllers
{
    public class AdminProductController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly DataContext _context;

        public AdminProductController(ProductRepository productRepository, DataContext context)
        {
            _productRepository = productRepository;
            _context = context;
        }

        public ActionResult Index(int sayfa=1)
        {
            var products = _context.Products
         .Include(x => x.Category)
         .ToPagedList(sayfa, 3);

            return View(products);
        }

        public ActionResult Create()
        {
            List<SelectListItem> deger1 = (from i in _context.Categorys.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.Name,
                                               Value = i.Id.ToString()
                                           }).ToList();
            ViewBag.ktgr = deger1;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product data, IFormFile file)
        {
            //httpposted resim yükleme işlemi için konur
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Hata Oluştu");
            }

            if (file != null && file.Length > 0)
            {
                string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Content", "Image");
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string filePath = Path.Combine(folder, file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream); // use CopyToAsync if method is async
                }

                data.Image = file.FileName;
            }
            _productRepository.Insert(data);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var delete = _productRepository.GetById(id);
            _productRepository.Delete(delete);
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            List<SelectListItem> deger1 = (from i in _context.Categorys.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.Name,
                                               Value = i.Id.ToString()
                                           }).ToList();
            ViewBag.ktgr = deger1;
            var update = _productRepository.GetById(id);
            //productRepository.Update(update);
            return View(update);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Update(Product data, IFormFile file)
        {
            //httpposted resim yükleme işlemi için konur
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Hata Oluştu");
            }

            var update = _productRepository.GetById(data.Id);
            update.Description = data.Description;
            update.Name = data.Name;
            update.IsApproved = data.IsApproved;
            update.Popular = data.Popular;
            update.Price = data.Price;
            update.Stock = data.Stock;
            update.Image = file.FileName.ToString();
            update.CategoryId = data.CategoryId;
            

            if (file != null && file.Length > 0)
            {
                string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Content", "Image");
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string filePath = Path.Combine(folder, file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream); // use CopyToAsync if method is async
                }

                data.Image = file.FileName;
            }
            _productRepository.Update(update);
            return RedirectToAction("Index");
        }

        public ActionResult CriticalStock()
        {
            var kritik = _context.Products.Where(x => x.Stock <= 50).ToList();
            return View(kritik);
        }
        public PartialViewResult StockCount()
        {
            if (User.Identity.IsAuthenticated)
            {
                var count = _context.Products.Where(x => x.Stock < 50).Count();
                ViewBag.count = count;
                var azalan = _context.Products.Where(x => x.Stock == 50).Count();
                ViewBag.azalan = azalan;
            }
            return PartialView();
        }
    }
}
