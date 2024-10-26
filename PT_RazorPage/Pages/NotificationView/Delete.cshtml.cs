using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Model;

namespace PT_RazorPage.Pages.NotificationView
{
    public class DeleteModel : PageModel
    {
        private readonly DataAccess.GymanyDbsContext _context;

        public DeleteModel(DataAccess.GymanyDbsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Notification Notification { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notification = await _context.Notifications.FirstOrDefaultAsync(m => m.NotiId == id);

            if (notification == null)
            {
                return NotFound();
            }
            else
            {
                Notification = notification;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notification = await _context.Notifications.FindAsync(id);
            if (notification != null)
            {
                Notification = notification;
                _context.Notifications.Remove(Notification);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
