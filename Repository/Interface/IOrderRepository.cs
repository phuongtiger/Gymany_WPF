using Model;

namespace Repository.Interface
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetListAll();
        Task<Order> GetById(int id);
        Task Add(Order item);
        Task Update(Order item);
        Task Delete(int id);
    }
}
