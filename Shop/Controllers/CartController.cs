using Microsoft.AspNetCore.Mvc;
using Shop.Core.Contracts;
using Shop.Core.Domain;

namespace Shop.Controllers
{
    public class CartController : Controller
    {
        private readonly IProdctFacade prodctFacade;
        private readonly Cart cart;

        public CartController(IProdctFacade prodctFacade, Cart cart)
        {
            this.prodctFacade = prodctFacade;
            this.cart = cart;
        }
        public IActionResult Index()
        {
            var cartCount = cart.CalculateCartCount();
            ViewBag.CartCount = cartCount;
            return View(cart);
        }
        public IActionResult AddToCart(int productId, int qunaity)
        {
            string referer = Request.Headers["Referer"].ToString();
            Product product = prodctFacade.Get(productId);
            if (product != null)
            {
                cart.AddItem(product, qunaity);
            }
            return Redirect(referer);
        }

        public IActionResult RemoveAtCart(int productId)
        {
            string referer = Request.Headers["Referer"].ToString();
            cart.RemoveLine(productId);
            return Redirect(referer);
        }
    }
}
