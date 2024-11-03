using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccess;
using Model;
using BussinessLogic.Interface;

namespace PT_RazorPage.Pages.PostView
{
    public class CreateModel : PageModel
    {
        private readonly IPostService _postService;

        public CreateModel(IPostService postService)
        {
            _postService = postService;
        }

        public IActionResult OnGet()
        {
            //ViewData["AdminId"] = new SelectList(_context.Admins, "AdminId", "AdminEmail");
            //ViewData["CusId"] = new SelectList(_context.Customers, "CusId", "CusEmail");
            //ViewData["PtId"] = new SelectList(_context.PersonalTrainers, "PtId", "PtEmail");
            Post = new Post
            {
                PostDate = DateTime.Today, // hoặc DateTime.Today nếu chỉ cần ngày
                CusId = 1,  // Default Customer ID
                PtId = 1,   // Default Personal Trainer ID
                AdminId = 1  // Default Admin ID
            };
            return Page();
        }

        [BindProperty]
        public Post Post { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
         
        await _postService.AddPost(Post);

            return RedirectToPage("./Index");
        }
    }
}
