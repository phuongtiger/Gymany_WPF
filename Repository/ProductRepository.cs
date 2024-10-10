using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DAOs;
using Model;
using Repository.Interface;

namespace Repository
{
    public class ProductRepository : IProductRepository
    {
        private ProductDAO productDAO;
        public ProductRepository(ProductDAO item)
        {
            productDAO = item;
        }

        public async Task<IEnumerable<Product>> GetListAll() => await productDAO.GetListAll();
        public async Task<Product> GetById(int id) => await productDAO.GetById(id);
        public async Task Add(Product item) => await productDAO.Add(item);
        public async Task Update(Product item) => await productDAO.Update(item);
        public async Task Delete(int id) => await productDAO.Delete(id);
    }
}
