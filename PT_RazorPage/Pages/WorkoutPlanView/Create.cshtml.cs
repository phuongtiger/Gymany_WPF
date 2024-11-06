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
using System.Collections.ObjectModel;

namespace PT_RazorPage.Pages.WorkoutPlanView
{
    public class CreateModel : PageModel
    {
        private readonly IWorkoutPlanService _service;
        private readonly ICustomerService _customerService;
        public ObservableCollection<Customer> Customers { get; set; }

        public CreateModel(IWorkoutPlanService service, ICustomerService customerService)
        {
            _service = service;
            _customerService = customerService;
            Customers = new ObservableCollection<Customer>();
            _ = LoadCustomer();
        }

        public async Task LoadCustomer()
        {
            try
            {
                Customers.Clear();
                var customers = await _customerService.GetListAllCustomer();
                foreach (var customer in customers)
                {
                    Customers.Add(customer);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [BindProperty]
        public WorkoutPlan WorkoutPlan { get; set; } = new WorkoutPlan();

        public IActionResult OnGet()
        {
            ViewData["CusId"] = new SelectList(Customers, "CusId", "CusName");
            // Set default values
            WorkoutPlan.ExcId = 1;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var ptId = HttpContext.Session.GetInt32("PtId");
            WorkoutPlan.PtId = ptId.Value;
            WorkoutPlan.ExcId = 1;
            await _service.AddWorkoutPlan(WorkoutPlan);

            return RedirectToPage("./Index");
        }
    }
}
