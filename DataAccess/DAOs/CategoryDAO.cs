using Microsoft.EntityFrameworkCore;
using Model;

namespace DataAccess.DAOs
{
    public class CategoryDAO : SingleTonBase<CategoryDAO>
    {
        public async Task<IEnumerable<Category>> GetListAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetById(int id)
        {
            var item = await _context.Categories.FirstOrDefaultAsync(c => c.CateId == id);
            if (item == null) return null;
            return item;
        }


        public async Task Add(Category item)
        {
            _context.Categories.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Category item)
        {
            var existingItem = await GetById(item.CateId);
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
                _context.Categories.Remove(item);
                await _context.SaveChangesAsync();
            }

        }
    }
}
