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
    public class AdminSalesController : Controller
    {
        private readonly DataContext _db;

        // 1) Constructor Injection
        public AdminSalesController(DataContext db)
        {
            _db = db;
        }
        public IActionResult Index(int sayfa = 1)
        {
            var sales = _db.Sales
                           .Include(s => s.Product)
                           .OrderByDescending(s => s.Date)
                           .ToPagedList(sayfa, 5);

            return View(sales);
        }
    }
}
