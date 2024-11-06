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

        public async Task<IList<WorkoutPlan>> GetListWorkoutPlansByPt(int ptId)
        {
            return await _context.WorkoutPlans
                .Include(w => w.Cus) // Include Customer entity
                .Include(w => w.Exc) // Include Exercise or related entity
                .Include(w => w.Pt)  // Include PersonalTrainer entity
                .Where(w => w.PtId == ptId) // Filter by PtId
                .ToListAsync();
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
            // Retrieve the existing workout plan along with the Exercise entity
            var existingItem = await _context.WorkoutPlans
                .Include(w => w.Exc) // Include related Exercise entity if necessary
                .FirstOrDefaultAsync(wp => wp.WorkoutId == item.WorkoutId);

            if (existingItem != null)
            {
                // Update properties, excluding foreign key properties if needed
                existingItem.WorkoutName = item.WorkoutName;
                existingItem.WorkoutDescription = item.WorkoutDescription;
                existingItem.WorkoutStartDate = item.WorkoutStartDate;
                existingItem.WorkoutEndDate = item.WorkoutEndDate;
                existingItem.WorkoutSession = item.WorkoutSession;
                existingItem.WorkoutActivity = item.WorkoutActivity;
                existingItem.ExcId = 1; // Set the Exercise ID if valid

                _context.WorkoutPlans.Update(existingItem);
                await _context.SaveChangesAsync();
            }
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
