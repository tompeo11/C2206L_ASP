using Microsoft.AspNetCore.Mvc;
using TEST.Models;
using TEST.Services;
using TEST.Helpers;
using TEST.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

        [HttpGet]
        public IActionResult GetAll()
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
            return Json(new {data = shoppingCartList});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCart([FromBody] List<UpdateCartDto> data)
        {
            if (HttpContext.Session.Get<List<BasketItem>>(ShoppingCartSessionVariable) != default)
            {
                HttpContext.Session.Get<List<BasketItem>>(ShoppingCartSessionVariable);

                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i].Count != 0)
                    {
                        int id = data[i].Id;
                        int newQuantity = data[i].Count;
                            
                        List<BasketItem> itemList = HttpContext.Session.Get<List<BasketItem>>(ShoppingCartSessionVariable);

                        if (itemList.Where(i => i.Product.Id == id).Any())
                        {
                            int currentQuantity = itemList.Where(i => i.Product.Id == id).Select(i => i.Count).FirstOrDefault();
                            int change = newQuantity - currentQuantity;
                            _basketService.AddItem(data[i].Id, change);
                        }
                    }
                    else
                    {
                        _basketService.RemoveItem(data[i].Id);
                    }
                }
            }
            return Json(new { redirectToUrl = Url.Action("Index", "Basket") });
        }
    }
}
