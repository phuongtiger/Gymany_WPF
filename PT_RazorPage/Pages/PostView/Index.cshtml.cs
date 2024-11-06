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

namespace PT_RazorPage.Pages.PostView
{
    public class IndexModel : PageModel
    {
        private readonly IPostService _postService;

        public IndexModel(IPostService postService)
        {
            _postService = postService;
        }

        public IList<Post> Post { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var ptId = HttpContext.Session.GetInt32("PtId");

            // Load workout plans associated with the PtId
            if (ptId.HasValue)
            {
                Post = await _postService.GetListPostByPt(ptId.Value);
            }
            else
            {
                Post = new List<Post>(); // Handle the case where PtId is not set
            }
        }
    }
}