using Microsoft.AspNetCore.Mvc;
using TEST.Models;
using TEST.Services;
using TEST.Helpers;

namespace TEST.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        const string ShoppingCartSessionVariable = "_ShoppingCartSessionVariable";

        public BasketController(IBasketService basketService) 
        {
            _basketService = basketService;
        }

        public IActionResult Index()
        {
            List<BasketItem> shoppingCartList;
            if (HttpContext.Session.Get<List<BasketItem>>(ShoppingCartSessionVariable) != default)
            {
                shoppingCartList = HttpContext.Session.Get<List<BasketItem>>(ShoppingCartSessionVariable);
            }
            else
            {
                shoppingCartList = new List<BasketItem>();
            }
            return View(shoppingCartList);
        }

        public IActionResult AddToCart(int id)
        {
            _basketService.AddItem(id, 1);
            return RedirectToAction("Index", "Home");
        }
    }
}
