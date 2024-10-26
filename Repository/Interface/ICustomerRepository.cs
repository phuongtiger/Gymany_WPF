using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository.Interface
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetListAll();
        Task<Customer> GetById(int id);
        Task Add(Customer item);
        Task Update(Customer item);
        Task Delete(int id);
    }
}
