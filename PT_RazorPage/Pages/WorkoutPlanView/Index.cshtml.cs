using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Model;
using BussinessLogic.Interface;

namespace PT_RazorPage.Pages.WorkoutPlanView
{
    public class IndexModel : PageModel
    {
        private readonly IWorkoutPlanService _service;

        public IndexModel(IWorkoutPlanService service)
        {
            _service = service;
        }

        public IList<WorkoutPlan> WorkoutPlan { get;set; } = default!;

        public async Task OnGetAsync()
        {
            // Retrieve the PtId from the session
            var ptId = HttpContext.Session.GetInt32("PtId");

            // Load workout plans associated with the PtId
            if (ptId.HasValue)
            {
                WorkoutPlan = await _service.GetListWorkoutPlansByPt(ptId.Value);
            }
            else
            {
                WorkoutPlan = new List<WorkoutPlan>(); // Handle the case where PtId is not set
            }
        }
    }
}
