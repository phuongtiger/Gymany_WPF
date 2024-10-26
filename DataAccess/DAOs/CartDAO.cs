using Microsoft.EntityFrameworkCore;
using Model;

namespace DataAccess.DAOs
{
    public class CartDAO : SingleTonBase<CartDAO>
    {
        public async Task<IEnumerable<Cart>> GetListAll()
        {
            return await _context.Carts.ToListAsync();
        }

        public async Task<Cart> GetById(int id)
        {
            var item = await _context.Carts.FirstOrDefaultAsync(c => c.CartId == id);
            if (item == null) return null;
            return item;
        }


        public async Task Add(Cart item)
        {
            _context.Carts.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Cart item)
        {
            var existingItem = await GetById(item.CartId);
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
                _context.Carts.Remove(item);
                await _context.SaveChangesAsync();
            }

        }
    }
}
