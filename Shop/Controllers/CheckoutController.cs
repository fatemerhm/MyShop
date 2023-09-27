using Microsoft.AspNetCore.Mvc;
using Shop.Core.Contracts;
using Shop.Core.Domain;

namespace Shop.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly Cart cart;
        private readonly IOrderFacade orderFacade;

        public CheckoutController(Cart cart, IOrderFacade orderFacade)
        {
            this.cart = cart;
            this.orderFacade = orderFacade;
        }
        public IActionResult Index()
        {
            ViewBag.cart = cart;
            return View(new Order());
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Index(Order order)
        {
            var totalPice = cart.GetTotalPrice();
            if (cart.CartLines.Count() == 0)
            {
                ModelState.AddModelError("", "سفارشی موجود نیست");
            }

            if (ModelState.IsValid)
            {
                order.Lines = cart.CartLines.ToList();
                orderFacade.SaveOrder(order);
                cart.Clear();
                return RedirectToAction("Pay", "Payment", new { orderId = order.OrderID, totalPrice = totalPice });
            }
            return View(order);
        }
    }
}
