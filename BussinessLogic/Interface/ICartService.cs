using Model;

namespace BussinessLogic.Interface
{
    public interface ICartService
    {
        Task<IEnumerable<Cart>> GetListAllCart();
        Task<Cart> GetByIdCart(int id);
        Task AddCart(Cart item);
        Task UpdateCart(Cart item);
        Task DeleteCart(int id);
    }
}
