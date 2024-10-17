using DataAccess.DAOs;
using Model;
using Repository.Interface;

namespace Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private CategoryDAO categoryDAO;
        public CategoryRepository(CategoryDAO item)
        {
            categoryDAO = item;
        }

        public async Task<IEnumerable<Category>> GetListAll() => await categoryDAO.GetListAll();
        public async Task<Category> GetById(int id) => await categoryDAO.GetById(id);
        public async Task Add(Category item) => await categoryDAO.Add(item);
        public async Task Update(Category item) => await categoryDAO.Update(item);
        public async Task Delete(int id) => await categoryDAO.Delete(id);
    }
}
