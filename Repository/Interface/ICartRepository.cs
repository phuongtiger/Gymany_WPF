using Model;

namespace Repository.Interface
{
    public interface ICartRepository
    {
        Task<IEnumerable<Cart>> GetListAll();
        Task<Cart> GetById(int id);
        Task Add(Cart item);
        Task Update(Cart item);
        Task Delete(int id);
    }
}
