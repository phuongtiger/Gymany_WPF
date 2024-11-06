using BussinessLogic.Interface;
using Model;
using Repository.Interface;

namespace BussinessLogic.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Customer>> GetListAllCustomer() => await _repository.GetListAll();
        public async Task<Customer> GetByIdCustomer(int id) => await _repository.GetById(id);
        public async Task AddCustomer(Customer item) => await _repository.Add(item);
        public async Task UpdateCustomer(Customer item) => await _repository.Update(item);
        public async Task DeleteCustomer(int id) => await _repository.Delete(id);
        public async Task<Customer> GetByUsernameCustomer(string username) => await _repository.GetByUsername(username);
    }
}
