using DataAccess.DAOs;
using Model;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class WorkoutPlanRepository : IWorkoutPlanRepository
    {
        private WorkoutPlanDAO workoutPlanDAO;
        public WorkoutPlanRepository(WorkoutPlanDAO item)
        {
            workoutPlanDAO = item;
        }

        public async Task<IEnumerable<WorkoutPlan>> GetListAll() => await workoutPlanDAO.GetListAll();
        public async Task<WorkoutPlan> GetById(int id) => await workoutPlanDAO.GetById(id);
        public async Task Add(WorkoutPlan item) => await workoutPlanDAO.Add(item);
        public async Task Update(WorkoutPlan item) => await workoutPlanDAO.Update(item);
        public async Task Delete(int id) => await workoutPlanDAO.Delete(id);
    }
}
