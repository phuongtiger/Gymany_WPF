using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Model;

namespace PT_RazorPage.Pages.WorkoutPlanView
{
    public class DetailsModel : PageModel
    {
        private readonly DataAccess.GymanyDbsContext _context;

        public DetailsModel(DataAccess.GymanyDbsContext context)
        {
            _context = context;
        }

        public WorkoutPlan WorkoutPlan { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutplan = await _context.WorkoutPlans.FirstOrDefaultAsync(m => m.WorkoutId == id);
            if (workoutplan == null)
            {
                return NotFound();
            }
            else
            {
                WorkoutPlan = workoutplan;
            }
            return Page();
        }
    }
}
