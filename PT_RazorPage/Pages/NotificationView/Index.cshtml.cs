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
    public class IndexModel : PageModel
    {
        private readonly DataAccess.GymanyDbsContext _context;

        public IndexModel(DataAccess.GymanyDbsContext context)
        {
            _context = context;
        }

        public IList<Notification> Notification { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Notification = await _context.Notifications
                .Include(n => n.Cus)
                .Include(n => n.Pt).ToListAsync();
        }
    }
}
