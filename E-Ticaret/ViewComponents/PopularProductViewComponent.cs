using BusinessLayer2.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.ViewComponents
{
    public class PopularProductViewComponent : ViewComponent
    {
        private readonly ProductRepository _productRepository;

        public PopularProductViewComponent(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IViewComponentResult Invoke()
        {
            var products = _productRepository.GetPopularProduct();
            return View("PopularProduct", products);
        }
    }
}
