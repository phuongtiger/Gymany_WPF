using Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAOs
{
    public class NotificationDAO : SingleTonBase<NotificationDAO>
    {
        public async Task<IEnumerable<Notification>> GetListAll()
        {
            return await _context.Notifications.ToListAsync();
        }

        public async Task<Notification> GetById(int id)
        {
            var item = await _context.Notifications.FirstOrDefaultAsync(c => c.NotiId == id);
            if (item == null) return null;
            return item;
        }


        public async Task Add(Notification item)
        {
            _context.Notifications.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Notification item)
        {
            var existingItem = await GetById(item.NotiId);
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
                _context.Notifications.Remove(item);
                await _context.SaveChangesAsync();
            }

        }
    }
}
