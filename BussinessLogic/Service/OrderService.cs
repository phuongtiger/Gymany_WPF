using BussinessLogic.Interface;
using Model;
using Repository;
using Repository.Interface;

namespace BussinessLogic.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Order>> GetListAllOrder() => await _orderRepository.GetListAll();
        public async Task<Order> GetByIdOrder(int id) => await _orderRepository.GetById(id);
        public async Task AddOrder(Order item) => await _orderRepository.Add(item);
        public async Task UpdateOrder(Order item) => await _orderRepository.Update(item);
        public async Task DeleteOrder(int id) => await _orderRepository.Delete(id);
        public async Task<Product> GetByIdProduct(int id) => await _productRepository.GetById(id);
    }
}
