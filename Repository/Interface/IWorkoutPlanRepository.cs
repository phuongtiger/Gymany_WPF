using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository.Interface
{
    public interface IWorkoutPlanRepository
    {
        Task<IEnumerable<WorkoutPlan>> GetListAll();
        Task<WorkoutPlan> GetById(int id);
        Task Add(WorkoutPlan item);
        Task Update(WorkoutPlan item);
        Task Delete(int id);

        Task<IList<WorkoutPlan>> GetListWorkoutPlansByPt(int ptId);
    }
}
