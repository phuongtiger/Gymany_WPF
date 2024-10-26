
using Microsoft.EntityFrameworkCore;
using Model;

namespace DataAccess.DAOs
{
    public class PersonalTrainerDAO : SingleTonBase<PersonalTrainerDAO>
    {
        public async Task<IEnumerable<PersonalTrainer>> GetListAll()
        {
            return await _context.PersonalTrainers.ToListAsync();
        }

        public async Task<PersonalTrainer> GetById(int id)
        {
            var item = await _context.PersonalTrainers.FirstOrDefaultAsync(c => c.PtId == id);
            if (item == null) return null;
            return item;
        }


        public async Task Add(PersonalTrainer item)
        {
            _context.PersonalTrainers.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(PersonalTrainer item)
        {
            var existingItem = await GetById(item.PtId);
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
                _context.PersonalTrainers.Remove(item);
                await _context.SaveChangesAsync();
            }

        }
    }
}
