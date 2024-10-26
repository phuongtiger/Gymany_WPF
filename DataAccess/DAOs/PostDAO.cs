using Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAOs
{
    public class PostDAO : SingleTonBase<PostDAO>
    {
        public async Task<IEnumerable<Post>> GetListAll()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<Post> GetById(int id)
        {
            var item = await _context.Posts.FirstOrDefaultAsync(c => c.PostId == id);
            if (item == null) return null;
            return item;
        }


        public async Task Add(Post item)
        {
            _context.Posts.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Post item)
        {
            var existingItem = await GetById(item.PostId);
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
                _context.Posts.Remove(item);
                await _context.SaveChangesAsync();
            }

        }
    }
}
