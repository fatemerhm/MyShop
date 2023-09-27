using Microsoft.AspNetCore.Mvc;
using Shop.Core.Contracts;
using Shop.Core.Domain;

namespace Shop.Components
{
    public class ChippestProductViewComponent: ViewComponent
    {
        private readonly IProductRepository productRepository;

        public ChippestProductViewComponent(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var prodcuts = productRepository.GetChippestProduct().Take(4).ToList();
            return View(prodcuts);
        }

    }
}
