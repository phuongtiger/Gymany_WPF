using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
    public class WorkoutPlanDAO : SingleTonBase<WorkoutPlanDAO>
    {
        public async Task<IEnumerable<WorkoutPlan>> GetListAll()
        {
            return await _context.WorkoutPlans.ToListAsync();
        }

        public async Task<WorkoutPlan> GetById(int id)
        {
            var item = await _context.WorkoutPlans.FirstOrDefaultAsync(c => c.WorkoutId == id);
            if (item == null) return null;
            return item;
        }


        public async Task Add(WorkoutPlan item)
        {
            _context.WorkoutPlans.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(WorkoutPlan item)
        {
            var existingItem = await GetById(item.WorkoutId);
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
                _context.WorkoutPlans.Remove(item);
                await _context.SaveChangesAsync();
            }

        }
    }
}
