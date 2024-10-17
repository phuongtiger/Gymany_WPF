using Model;

namespace BussinessLogic.Interface
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetListAllOrder();
        Task<Order> GetByIdOrder(int id);
        Task AddOrder(Order item);
        Task UpdateOrder(Order item);
        Task DeleteOrder(int id);
        Task<Product> GetByIdProduct(int id);
    }
}
