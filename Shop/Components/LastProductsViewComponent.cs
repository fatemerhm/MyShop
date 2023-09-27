using Microsoft.AspNetCore.Mvc;
using Shop.Core.Contracts;

namespace Shop.Components
{
    public class LastProductsViewComponent:ViewComponent
    {
        private readonly IProductRepository productRepository;

        public LastProductsViewComponent(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = productRepository.GetNewstProduct().Take(8).ToList();
            return View(data);
        }
    }
}
