using DataAccess.DAOs;
using Model;
using Repository.Interface;

namespace Repository
{
    public class OrderRepository : IOrderRepository
    {
        private OrderDAO orderDAO;
        public OrderRepository(OrderDAO item)
        {
            orderDAO = item;
        }

        public async Task<IEnumerable<Order>> GetListAll() => await orderDAO.GetListAll();
        public async Task<Order> GetById(int id) => await orderDAO.GetById(id);
        public async Task Add(Order item) => await orderDAO.Add(item);
        public async Task Update(Order item) => await orderDAO.Update(item);
        public async Task Delete(int id) => await orderDAO.Delete(id);

    }
}
