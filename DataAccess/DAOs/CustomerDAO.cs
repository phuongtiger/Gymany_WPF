using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DataAccess.DAOs
{
    public class CustomerDAO : SingleTonBase<CustomerDAO>
    {
        public async Task<IEnumerable<Customer>> GetListAll()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetById(int id)
        {
            var item = await _context.Customers.FirstOrDefaultAsync(c => c.CusId == id);
            if (item == null) return null;
            return item;
        }


        public async Task Add(Customer item)
        {
            _context.Customers.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Customer item)
        {
            var existingItem = await GetById(item.CusId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(item);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var item = await GetById(id);
            if (item != null)
            {
                _context.Customers.Remove(item);
                await _context.SaveChangesAsync();
            }

        }
    }
}
