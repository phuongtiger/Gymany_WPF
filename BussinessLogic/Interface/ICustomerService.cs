
using Model;

namespace BussinessLogic.Interface
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetListAllCustomer();
        Task<Customer> GetByIdCustomer(int id);
        Task AddCustomer(Customer item);
        Task UpdateCustomer(Customer item);
        Task DeleteCustomer(int id);
    }
}
