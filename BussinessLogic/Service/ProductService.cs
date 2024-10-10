using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLogic.Interface;
using Model;
using Repository.Interface;

namespace BussinessLogic.Service
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetListAllProduct() => await _productRepository.GetListAll();
        public async Task<Product> GetByIdProduct(int id) => await _productRepository.GetById(id);
        public async Task AddProduct(Product item) => await _productRepository.Add(item);
        public async Task UpdateProduct(Product item) => await _productRepository.Update(item);
        public async Task DeleteProduct(int id) => await _productRepository.Delete(id);
    }
}
