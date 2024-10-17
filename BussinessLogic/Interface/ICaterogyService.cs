using Model;

namespace BussinessLogic.Interface
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetListAllCategory();
        Task<Category> GetByIdCategory(int id);
        Task AddCategory(Category item);
        Task UpdateCategory(Category item);
        Task DeleteCategory(int id);
    }
}
