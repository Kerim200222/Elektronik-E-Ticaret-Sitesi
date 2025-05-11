using Microsoft.AspNetCore.Mvc;
using DataAccessLayer2.Context;
using BusinessLayer2.Concrete;

namespace E_Ticaret.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DataContext _context;
        private readonly CategoryRepository _categoryRepository;

        public CategoryController(DataContext context, CategoryRepository categoryRepository)
        {
            _context = context;
            _categoryRepository = categoryRepository;
        }

        public PartialViewResult CategoryList()
        {
            var categories = _context.Categorys.ToList();
            return PartialView("CategoryListPartial", categories);
        }

        public ActionResult Details(int id)
        {
            var cat = _categoryRepository.CategoryDetails(id);
            return View(cat);
        }
    }
}
