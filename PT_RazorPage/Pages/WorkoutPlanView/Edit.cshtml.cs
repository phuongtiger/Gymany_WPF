using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Model;

namespace PT_RazorPage.Pages.WorkoutPlanView
{
    public class EditModel : PageModel
    {
        private readonly DataAccess.GymanyDbsContext _context;

        public EditModel(DataAccess.GymanyDbsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public WorkoutPlan WorkoutPlan { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutplan =  await _context.WorkoutPlans.FirstOrDefaultAsync(m => m.WorkoutId == id);
            if (workoutplan == null)
            {
                return NotFound();
            }
            WorkoutPlan = workoutplan;
           ViewData["CusId"] = new SelectList(_context.Customers, "CusId", "CusEmail");
           ViewData["ExcId"] = new SelectList(_context.Exercises, "ExcId", "ExcDescription");
           ViewData["PtId"] = new SelectList(_context.PersonalTrainers, "PtId", "PtEmail");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(WorkoutPlan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkoutPlanExists(WorkoutPlan.WorkoutId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool WorkoutPlanExists(int id)
        {
            return _context.WorkoutPlans.Any(e => e.WorkoutId == id);
        }
    }
}
