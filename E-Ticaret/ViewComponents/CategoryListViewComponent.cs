using DataAccessLayer2.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace E_Ticaret.ViewComponents
{
    public class CategoryListViewComponent : ViewComponent
    {
        private readonly DataContext _context;

        public CategoryListViewComponent(DataContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _context.Categorys
               .Include(c => c.Products)
               .ToList();

            return View("CategoryListPartial", categories);
        }
    }
}