using Microsoft.AspNetCore.Mvc;
using ViewComponentInMVC.Models;

namespace ViewComponentInMVC.ViewComponents
{
    public class TopProductsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            ProductRepository productRepository = new ProductRepository();
            var products = await productRepository.GetTopProductsAsync(count);
            return View(products);
        }
    }
}
