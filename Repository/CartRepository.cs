using Model;
using DataAccess.DAOs;
using Repository.Interface;

namespace Repository
{
    public class CartRepository : ICartRepository
    {
        private CartDAO categoryDAO;
        public CartRepository(CartDAO item)
        {
            categoryDAO = item;
        }

        public async Task<IEnumerable<Cart>> GetListAll() => await categoryDAO.GetListAll();
        public async Task<Cart> GetById(int id) => await categoryDAO.GetById(id);
        public async Task Add(Cart item) => await categoryDAO.Add(item);
        public async Task Update(Cart item) => await categoryDAO.Update(item);
        public async Task Delete(int id) => await categoryDAO.Delete(id);
    }
}
