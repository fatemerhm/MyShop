using Microsoft.AspNetCore.Mvc;
using Shop.Core.Contracts;
using Shop.Core.Domain;
using Shop.Models;
using System.Diagnostics;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProdctFacade prodctFacade;

        private readonly Cart cart;

        public HomeController(IProdctFacade prodctFacade, Cart cart)
        {
            this.prodctFacade = prodctFacade;
            this.cart = cart;
        }

        public IActionResult Index(int page = 1, string category = "All", string q = "")
        {
            var cartCount = cart.CalculateCartCount();
            ViewBag.CartCount = cartCount;
            var data = prodctFacade.ProductSearch(q, category, page, 8);
            PagedList<Product> pageList = new PagedList<Product>(data.Item1, page, 8, data.Item2);
            ViewBag.category = category;
            ViewBag.q = q;
            return View(pageList);
        }

        public IActionResult Home(int page = 1)
        {
            var cartCount = cart.CalculateCartCount();
            ViewBag.CartCount = cartCount;
            var prodcuts = prodctFacade.GetAll(page, 8);
            PagedList<Product> pageList = new PagedList<Product>(prodcuts.Item1, page, 8, prodcuts.Item2);
            return View(pageList);
        }
        public IActionResult SingelProduct(int id)
        {
            var cartCount = cart.CalculateCartCount();
            ViewBag.CartCount = cartCount;
            Product product = prodctFacade.Get(id);
            return View(product);
        }
        public IActionResult GetCloth(int Categoryid,int page = 1)
        {
            var cartCount = cart.CalculateCartCount();
            ViewBag.CartCount = cartCount;
            var prodcuts = prodctFacade.GetCategory(1, page, 8);
            PagedList<Product> pageList = new PagedList<Product>(prodcuts.Item1, page, 8, prodcuts.Item2);
            return View(pageList);
        }
        public IActionResult GetAccs(int Categoryid, int page = 1)
        {
            var cartCount = cart.CalculateCartCount();
            ViewBag.CartCount = cartCount;
            var prodcuts = prodctFacade.GetCategory(3, page, 8);
            PagedList<Product> pageList = new PagedList<Product>(prodcuts.Item1, page, 8, prodcuts.Item2);
            return View(pageList);
        }
        public IActionResult GetBag(int Categoryid, int page = 1)
        {
            var cartCount = cart.CalculateCartCount();
            ViewBag.CartCount = cartCount;
            var prodcuts = prodctFacade.GetCategory(2,page, 8);
            PagedList<Product> pageList = new PagedList<Product>(prodcuts.Item1, page, 8, prodcuts.Item2);
            return View(pageList);
        }
        public IActionResult GetBeau(int Categoryid, int page = 1)
        {
            var cartCount = cart.CalculateCartCount();
            ViewBag.CartCount = cartCount;
            var prodcuts = prodctFacade.GetCategory(4,page,8);
            PagedList<Product> pageList = new PagedList<Product>(prodcuts.Item1, page, 8, prodcuts.Item2);
            return View(pageList);
        }

        public IActionResult AboutUs()
        {
            var cartCount = cart.CalculateCartCount();
            ViewBag.CartCount = cartCount;
            return View();
        }

        public IActionResult Search(int page = 1, string category = "All", string q = "")
        {
            var data = prodctFacade.ProductSearch(q, category, page, 8);
            PagedList<Product> pageList = new PagedList<Product>(data.Item1, page, 8, data.Item2);
            ViewBag.category = category;
            ViewBag.q = q;
            return View(pageList);
        }
        public IActionResult GetChippestProductAll()
        {
            var prodcuts = prodctFacade.GetChippestProductAll().ToList();
            return View(prodcuts);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}