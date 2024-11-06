using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BussinessLogic.Interface;
using Model;
using Microsoft.Extensions.Hosting;

namespace PT_RazorPage.Pages.PostView
{
    public class CreateModel : PageModel
    {
        private readonly IPostService _postService;
        private readonly IWebHostEnvironment _environment;

        // Inject IWebHostEnvironment to access WebRootPath for file storage
        public CreateModel(IPostService postService, IWebHostEnvironment environment)
        {
            _postService = postService;
            _environment = environment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Post Post { get; set; } = default!;

        // This will handle the image file upload
        [BindProperty]
        public IFormFile PostImg { get; set; } // This property captures the uploaded file

        public async Task<IActionResult> OnPostAsync()
        {
            if (PostImg != null && PostImg.Length > 0)
            {
                // Generate a unique filename to avoid overwriting existing files
                var fileName = Path.GetFileName(PostImg.FileName);

                // Combine path to the folder where images will be stored (in wwwroot/uploads)
                var filePath = Path.Combine(_environment.WebRootPath, "uploads", fileName);

                // Create the uploads folder if it doesn't exist
                if (!Directory.Exists(Path.Combine(_environment.WebRootPath, "uploads")))
                {
                    Directory.CreateDirectory(Path.Combine(_environment.WebRootPath, "uploads"));
                }

                // Save the uploaded file to the server
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await PostImg.CopyToAsync(fileStream);
                }

                // Save the relative image path in the Post object
                Post.PostImg = "/uploads/" + fileName; // Relative path to be stored in DB
            }

            // Set the other properties of the Post
            var ptId = HttpContext.Session.GetInt32("PtId");
            Post.PostDate = DateTime.Now;
            Post.CusId = 1;  // Default Customer ID (replace with actual logic if needed)
            Post.PtId = ptId ?? 1;  // Default Personal Trainer ID
            Post.AdminId = 1; // Default Admin ID

            // Save the Post data to the database using your service
            await _postService.AddPost(Post);

            // Redirect to the Index page after creating the post
            return RedirectToPage("./Index");
        }
    }
}
