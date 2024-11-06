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
    public class DeleteModel : PageModel
    {
        private readonly IWorkoutPlanService _service;

        public DeleteModel(IWorkoutPlanService service)
        {
            _service = service;
        }

        [BindProperty]
        public WorkoutPlan WorkoutPlan { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutplan = await _service.GetByIdWorkoutPlan(id);

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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
                await _service.DeleteWorkoutPlan(id);
            return RedirectToPage("./Index");
        }
    }
}
