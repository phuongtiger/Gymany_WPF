using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess; // Ensure this namespace is correct
using Model;
using BussinessLogic.Interface; // Ensure your model namespace

public class IndexModel : PageModel
{
    private readonly IPersonalTrainerService _service;

    public IndexModel(IPersonalTrainerService service)
    {
        _service = service;
    }

    public PersonalTrainer PersonalTrainer { get; set; } = new PersonalTrainer();

    public async Task<IActionResult> OnGetAsync()
    {
        int ptId = (int)HttpContext.Session.GetInt32("PtId");

        // Retrieve Personal Trainer details from the database
        PersonalTrainer = await _service.GetByIdPersonalTrainer(ptId);

        if (PersonalTrainer == null)
        {
            return NotFound("Personal Trainer not found.");
        }

        return Page();
    }

    public IActionResult OnPostLogout()
    {
        // Clear the session
        HttpContext.Session.Remove("PtId");

        // Redirect to the login page
        return RedirectToPage("/Login");
    }
}
