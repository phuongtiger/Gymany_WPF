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
    public class CartService : ICartService
    {
        private readonly ICartRepository _repository;
        public CartService(ICartRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Cart>> GetListAllCart() => await _repository.GetListAll();
        public async Task<Cart> GetByIdCart(int id) => await _repository.GetById(id);
        public async Task AddCart(Cart item) => await _repository.Add(item);
        public async Task UpdateCart(Cart item) => await _repository.Update(item);
        public async Task DeleteCart(int id) => await _repository.Delete(id);
    }
}
