using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DataAccess.DAOs
{
    public class PaymentDAO : SingleTonBase<PaymentDAO>
    {
        public async Task<IEnumerable<Payment>> GetListAll()
        {
            return await _context.Payments.ToListAsync();
        }

        public async Task<Payment> GetById(int id)
        {
            var item = await _context.Payments.FirstOrDefaultAsync(c => c.PayId == id);
            if (item == null) return null;
            return item;
        }


        public async Task Add(Payment item)
        {
            _context.Payments.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Payment item)
        {
            var existingItem = await GetById(item.PayId);
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
                _context.Payments.Remove(item);
                await _context.SaveChangesAsync();
            }

        }
    }
}
