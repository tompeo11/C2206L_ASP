namespace TEST.Services
{
    public interface IBasketService
    {
        void AddItem(int id, int quantity);
        void RemoveItem(int id);
        void ClearBasket();
    }
}
