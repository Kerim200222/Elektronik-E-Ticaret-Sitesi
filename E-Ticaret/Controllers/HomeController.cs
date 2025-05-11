using BusinessLayer2.Concrete;
using DataAccessLayer2.Context;
using E_Ticaret.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;
using X.PagedList.Extensions;

namespace E_Ticaret.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductRepository _productRepository;
        private readonly DataContext _context;

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
            _productRepository = new ProductRepository(_context);
            _context = context;
        }

        public PartialViewResult CategoryList()
        {
            var categorys = _context.Categorys.ToList();
            return PartialView("CategoryListPartial", categorys);
        }

        public IActionResult Index(int sayfa =1)
        {
            return View(_productRepository.List().ToPagedList(sayfa, 3));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
