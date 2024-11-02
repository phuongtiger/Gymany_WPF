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
    public class IndexModel : PageModel
    {
        private readonly DataAccess.GymanyDbsContext _context;

        public IndexModel(DataAccess.GymanyDbsContext context)
        {
            _context = context;
        }

        public IList<WorkoutPlan> WorkoutPlan { get;set; } = default!;

        public async Task OnGetAsync()
        {
            WorkoutPlan = await _context.WorkoutPlans
                .Include(w => w.Cus)
                .Include(w => w.Exc)
                .Include(w => w.Pt).ToListAsync();
        }
    }
}
