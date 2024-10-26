using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Model;

namespace PT_RazorPage.Pages.PostView
{
    public class IndexModel : PageModel
    {
        private readonly DataAccess.GymanyDbsContext _context;

        public IndexModel(DataAccess.GymanyDbsContext context)
        {
            _context = context;
        }

        public IList<Post> Post { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Post = await _context.Posts
                .Include(p => p.Admin)
                .Include(p => p.Cus)
                .Include(p => p.Pt).ToListAsync();
        }
    }
}
