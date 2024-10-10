using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetListAll();
        Task<Product> GetById(int id);
        Task Add(Product item);
        Task Update(Product item);
        Task Delete(int id);
    }
}
