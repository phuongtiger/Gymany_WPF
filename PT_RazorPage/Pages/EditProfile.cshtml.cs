using BussinessLogic.Interface;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Model;
using System.IO;

namespace PT_RazorPage.Pages
{
    public class EditProfileModel : PageModel
    {
        private readonly IPersonalTrainerService _service;

        public EditProfileModel(IPersonalTrainerService service)
        {
            _service = service;
        }

        [BindProperty]
        public PersonalTrainer PersonalTrainer { get; set; }

        [BindProperty]
        public IFormFile? NewProfileImage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            PersonalTrainer = await _service.GetByIdPersonalTrainer(id);
            if (PersonalTrainer == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Fetch the PersonalTrainer ID from the session
            int ptId = (int)HttpContext.Session.GetInt32("PtId");

            // Retrieve the existing trainer from the service
            var existingTrainer = await _service.GetByIdPersonalTrainer(ptId);
            if (existingTrainer == null)
            {
                return NotFound();
            }

            // Update the trainer's properties with the new form data
            existingTrainer.PtName = PersonalTrainer.PtName;
            existingTrainer.PtAge = PersonalTrainer.PtAge;
            existingTrainer.PtAddress = PersonalTrainer.PtAddress;
            existingTrainer.PtEmail = PersonalTrainer.PtEmail;
            existingTrainer.PtPhone = PersonalTrainer.PtPhone;

            // Handle profile image upload
            if (NewProfileImage != null)
            {
                // Define the fixed path for the image
                var folderPath = @"E:\SE1707_Ky7\Project_GYMANY\Gymany\wwwroot\images\trainers";

                // Ensure the folder exists
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Generate the file path using the fixed directory
                var fileName = Path.GetFileName(NewProfileImage.FileName);
                var filePath = Path.Combine(folderPath, fileName);

                // Save the file to the fixed directory
                using var stream = new FileStream(filePath, FileMode.Create);
                await NewProfileImage.CopyToAsync(stream);

                // Update the trainer's profile image path with the fixed link
                existingTrainer.PtImg = Path.Combine("E:", "SE1707_Ky7", "Project_GYMANY", "Gymany", "wwwroot", "images", "Product", fileName);
            }

            // Update the trainer in the database
            await _service.UpdatePersonalTrainer(existingTrainer);

            // Redirect to the Index page after successful update
            return RedirectToPage("Index");
        }
    }
}
