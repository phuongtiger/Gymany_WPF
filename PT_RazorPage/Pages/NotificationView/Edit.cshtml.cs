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

namespace PT_RazorPage.Pages.NotificationView
{
    public class EditModel : PageModel
    {
        private readonly DataAccess.GymanyDbsContext _context;

        public EditModel(DataAccess.GymanyDbsContext context)
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

            var notification =  await _context.Notifications.FirstOrDefaultAsync(m => m.NotiId == id);
            if (notification == null)
            {
                return NotFound();
            }
            Notification = notification;
           ViewData["CusId"] = new SelectList(_context.Customers, "CusId", "CusEmail");
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

            _context.Attach(Notification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationExists(Notification.NotiId))
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

        private bool NotificationExists(int id)
        {
            return _context.Notifications.Any(e => e.NotiId == id);
        }
    }
}
