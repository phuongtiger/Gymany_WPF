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
using BussinessLogic.Interface;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace PT_RazorPage.Pages.WorkoutPlanView
{
    public class EditModel : PageModel
    {
        private readonly IWorkoutPlanService _service;

        public EditModel(IWorkoutPlanService service)
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
            WorkoutPlan = await _service.GetByIdWorkoutPlan(id);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            await _service.UpdateWorkoutPlan(WorkoutPlan);
            return RedirectToPage("./Index");
        }
    }
}
