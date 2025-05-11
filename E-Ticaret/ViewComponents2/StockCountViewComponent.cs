using Microsoft.AspNetCore.Mvc;
using DataAccessLayer2.Context;
using System.Linq;

namespace E_Ticaret.ViewComponents
{
    public class StockCountViewComponent:ViewComponent
    {
        private readonly DataContext _context;

        public StockCountViewComponent(DataContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var count = _context.Products.Count(p => p.Stock <= 10);
            return View(count);
        }
    }
}
