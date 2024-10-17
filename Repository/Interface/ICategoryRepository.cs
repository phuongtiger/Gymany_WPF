using Model;

namespace Repository.Interface
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetListAll();
        Task<Category> GetById(int id);
        Task Add(Category item);
        Task Update(Category item);
        Task Delete(int id);
    }
}
