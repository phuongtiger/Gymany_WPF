using BussinessLogic.Interface;
using Model;
using Repository;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Service
{
    public class WorkoutPlanService : IWorkoutPlanService
    {
        private readonly IWorkoutPlanRepository _workoutPlanRepository;
        public WorkoutPlanService(IWorkoutPlanRepository workoutPlanRepository)
        {
            _workoutPlanRepository = workoutPlanRepository;
        }

        public async Task<IEnumerable<WorkoutPlan>> GetListAllWorkoutPlan() => await _workoutPlanRepository.GetListAll();
        public async Task<WorkoutPlan> GetByIdWorkoutPlan(int id) => await _workoutPlanRepository.GetById(id);
        public async Task AddWorkoutPlan(WorkoutPlan item) => await _workoutPlanRepository.Add(item);
        public async Task UpdateWorkoutPlan(WorkoutPlan item) => await _workoutPlanRepository.Update(item);
        public async Task DeleteWorkoutPlan(int id) => await _workoutPlanRepository.Delete(id);
        public async Task<IList<WorkoutPlan>> GetListWorkoutPlansByPt(int ptId) => await _workoutPlanRepository.GetListWorkoutPlansByPt(ptId);
    }
}
