using Microsoft.AspNetCore.Http;
using TEST.DAO;
using TEST.Helpers;
using TEST.Models;

namespace TEST.Services
{
    public class BasketService : IBasketService
    {
        const string ShoppingCartSessionVariable = "_ShoppingCartSessionVariable";
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;

        public BasketService(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor) 
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
        }

        public void AddItem(int id, int quantity)
        {
            List<BasketItem> shoppingCartList;
            if (_contextAccessor.HttpContext.Session.Get<List<BasketItem>>(ShoppingCartSessionVariable) != default) 
            {
                shoppingCartList = _contextAccessor.HttpContext.Session.Get<List<BasketItem>>(ShoppingCartSessionVariable);
                
                if (shoppingCartList.Where(i => i.Product.Id == id).Any())
                {
                    shoppingCartList.Where(i => i.Product.Id == id).Select(x =>
                    {
                        x.Count += quantity;
                        return x;
                    }).ToList();
                }else
                {
                    shoppingCartList.Add(new BasketItem
                    {
                        Count = quantity,
                        Product = _unitOfWork.productRepository.GetEntityById(id)
                    });
                }
            }else
            {
                shoppingCartList = new List<BasketItem>
                {
                    new BasketItem
                    {
                        Count = quantity,
                        Product = _unitOfWork.productRepository.GetEntityById(id)
                    }
                };
            }

            _contextAccessor.HttpContext.Session.Set<List<BasketItem>>(ShoppingCartSessionVariable, shoppingCartList);
        }

        
        public void RemoveItem(int id)
        {
            if (_contextAccessor.HttpContext.Session.Get<List<BasketItem>>(ShoppingCartSessionVariable) != default)
            {
                List<BasketItem> shoppingCartList = _contextAccessor.HttpContext.Session.Get<List<BasketItem>>(ShoppingCartSessionVariable);

                shoppingCartList.RemoveAll(i => i.Product.Id == id);

                _contextAccessor.HttpContext.Session.Set<List<BasketItem>>(ShoppingCartSessionVariable, shoppingCartList);
            }
        }

        public void ClearBasket()
        {
            throw new NotImplementedException();
        }

    }
}
