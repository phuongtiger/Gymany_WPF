using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Interface
{
    public interface IWorkoutPlanService
    {
        Task<IEnumerable<WorkoutPlan>> GetListAllWorkoutPlan();
        Task<WorkoutPlan> GetByIdWorkoutPlan(int id);
        Task AddWorkoutPlan(WorkoutPlan item);
        Task UpdateWorkoutPlan(WorkoutPlan item);
        Task DeleteWorkoutPlan(int id);
        Task<IList<WorkoutPlan>> GetListWorkoutPlansByPt(int ptId);
    }
}
