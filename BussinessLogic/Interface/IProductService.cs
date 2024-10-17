using Model;

namespace BussinessLogic.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetListAllProduct();
        Task<Product> GetByIdProduct(int id);
        Task AddProduct(Product item);
        Task UpdateProduct(Product item);
        Task DeleteProduct(int id);
    }
}
