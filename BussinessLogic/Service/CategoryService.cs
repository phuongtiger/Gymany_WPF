using BussinessLogic.Interface;
using Model;
using Repository.Interface;


namespace BussinessLogic.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetListAllCategory() => await _categoryRepository.GetListAll();
        public async Task<Category> GetByIdCategory(int id) => await _categoryRepository.GetById(id);
        public async Task AddCategory(Category item) => await _categoryRepository.Add(item);
        public async Task UpdateCategory(Category item) => await _categoryRepository.Update(item);
        public async Task DeleteCategory(int id) => await _categoryRepository.Delete(id);
    }
}
