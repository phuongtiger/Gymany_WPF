using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace PT_RazorPage.Pages
{
    public class LoginModel : PageModel
    {
        private readonly GymanyDbsContext _context;

        public LoginModel(GymanyDbsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var trainer = await _context.PersonalTrainers
                .FirstOrDefaultAsync(t => t.PtUsername == Input.Username && t.PtPassword == Input.Password);

            if (trainer != null)
            {
                HttpContext.Session.SetInt32("PtId", trainer.PtId);

                // Implement login logic here (e.g., setting session, cookies)
                // Redirect to another page upon successful login
                return RedirectToPage("/Index"); // Redirect to your desired page
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }
        }
    }
    }

