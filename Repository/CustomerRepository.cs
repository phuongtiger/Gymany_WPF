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
    public class CustomerRepository : ICustomerRepository
    {
        private CustomerDAO DAO;
        public CustomerRepository(CustomerDAO item)
        {
            DAO = item;
        }

        public async Task<IEnumerable<Customer>> GetListAll() => await DAO.GetListAll();
        public async Task<Customer> GetById(int id) => await DAO.GetById(id);
        public async Task Add(Customer item) => await DAO.Add(item);
        public async Task Update(Customer item) => await DAO.Update(item);
        public async Task Delete(int id) => await DAO.Delete(id);
    }
}
