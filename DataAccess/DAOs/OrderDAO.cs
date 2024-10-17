using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DataAccess.DAOs
{
    public class OrderDAO : SingleTonBase<OrderDAO>
    {
        public async Task<IEnumerable<Order>> GetListAll()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetById(int id)
        {
            var item = await _context.Orders.FirstOrDefaultAsync(c => c.OrderId == id);
            if (item == null) return null;
            return item;
        }


        public async Task Add(Order item)
        {
            _context.Orders.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Order item)
        {
            var existingItem = await GetById(item.OrderId);
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
                _context.Orders.Remove(item);
                await _context.SaveChangesAsync();
            }

        }
    }
}
