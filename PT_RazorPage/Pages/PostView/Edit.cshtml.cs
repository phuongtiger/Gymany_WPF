using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccess;
using Model;
using BussinessLogic.Interface;

namespace PT_RazorPage.Pages.PostView
{
    public class EditModel : PageModel
    {
        private readonly IPostService _postService;


        public EditModel(IPostService postService)
        {
            _postService = postService;
        }

        [BindProperty]
        public Post Post { get; set; } = new Post(); // Initialize to avoid null reference

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Post = await _postService.GetByIdPost(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var existingPost = await _postService.GetByIdPost(Post.PostId);


            // Update properties from the bound Post
            existingPost.PostContent = Post.PostContent;
            existingPost.PostTitle = Post.PostTitle;
            existingPost.PostImg = Post.PostImg;

            // Update the post using the service
            await _postService.UpdatePost(existingPost);
            return RedirectToPage("./Index");
        }
    }
}
