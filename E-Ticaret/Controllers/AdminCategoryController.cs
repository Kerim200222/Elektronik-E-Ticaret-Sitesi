using BusinessLayer2.Concrete;
using EntityLayer22.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.Controllers
{
    [Authorize(Roles = "Admin")]

    public class AdminCategoryController : Controller
    {
        private readonly CategoryRepository _categoryRepository;

        public AdminCategoryController(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            return View(_categoryRepository.List());
        }

        public ActionResult Create()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(Category p)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Insert(p);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Bir Hata Oluştu");
            return View(p);
        }

        public ActionResult Delete(int id)
        {
            var delete = _categoryRepository.GetById(id);
            _categoryRepository.Delete(delete);
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            var update = _categoryRepository.GetById(id);
            return View(update);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Update(Category p)
        {
            if (ModelState.IsValid)
            {
                var update = _categoryRepository.GetById(p.Id);
                update.Name = p.Name;
                update.Description = p.Description;
                _categoryRepository.Update(update);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Bir hata oluştu");
            return View();

        }
    }
}
