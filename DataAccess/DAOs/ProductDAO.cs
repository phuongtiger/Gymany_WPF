using System;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DataAccess.DAOs
{
    public class ProductDAO : SingleTonBase<ProductDAO>
    {
        public async Task<IEnumerable<Product>> GetListAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            var item = await _context.Products.FirstOrDefaultAsync(c => c.ProdId == id);
            if (item == null) return null;
            return item;
        }


        public async Task Add(Product item)
        {
            _context.Products.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Product item)
        {
            var existingItem = await GetById(item.ProdId);
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
                _context.Products.Remove(item);
                await _context.SaveChangesAsync();
            }

        }
    }
}
