using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccess;
using Model;

namespace PT_RazorPage.Pages.NotificationView
{
    public class CreateModel : PageModel
    {
        private readonly DataAccess.GymanyDbsContext _context;

        public CreateModel(DataAccess.GymanyDbsContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CusId"] = new SelectList(_context.Customers, "CusId", "CusEmail");
        ViewData["PtId"] = new SelectList(_context.PersonalTrainers, "PtId", "PtEmail");
            return Page();
        }

        [BindProperty]
        public Notification Notification { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Notifications.Add(Notification);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
