using BusinessLayer2.Concrete;
using EntityLayer22.Entities;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;

        public ProductController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public PartialViewResult PopularProduct()
        {
            var product = _productRepository.GetPopularProduct();
            ViewBag.popular = product;
            return PartialView(product);
        }

        public ActionResult ProductDetails(int id)
        {
            var details = _productRepository.GetById(id);
            return View(details);   
        }

        public PartialViewResult ProductDetailsPartial(int id)
        {
            var prod = _productRepository.GetById(id);
            if (prod == null)
                
                return PartialView("_ProductDetailsPartial", new Product());

            return PartialView("_ProductDetailsPartial", prod);
        }
    }
}
