using System;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DataAccess.DAOs
{
    public class AdminDAO : SingleTonBase<AdminDAO>
    {
        public async Task<IEnumerable<Admin>> GetListAll()
        {
            return await _context.Admins.ToListAsync();
        }

        public async Task<Admin> GetById(int id)
        {
            var item = await _context.Admins.FirstOrDefaultAsync(c => c.AdminId == id);
            if (item == null) return null;
            return item;
        }


        public async Task Add(Admin item)
        {
            _context.Admins.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Admin item)
        {
            var existingItem = await GetById(item.AdminId);
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
                _context.Admins.Remove(item);
                await _context.SaveChangesAsync();
            }

        }
    }
}
