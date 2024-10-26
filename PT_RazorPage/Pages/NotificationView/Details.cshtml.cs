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
    public class DetailsModel : PageModel
    {
        private readonly DataAccess.GymanyDbsContext _context;

        public DetailsModel(DataAccess.GymanyDbsContext context)
        {
            _context = context;
        }

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
    }
}
