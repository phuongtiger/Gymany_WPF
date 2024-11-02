using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BussinessLogic.Interface;
using Model;

namespace PT_RazorPage.Pages.NotificationView
{
    public class EditModel : PageModel
    {
        public ObservableCollection<Customer> Customers { get; set; }
        private readonly INotificationService _notificationService;
        private readonly ICustomerService _customerService;

        public EditModel(INotificationService notificationService, ICustomerService customerService)
        {
            _notificationService = notificationService;
            _customerService = customerService;
            Customers = new ObservableCollection<Customer>();
        }

        public async Task OnGetAsync(int id)
        {
            await LoadCustomer(); // Make sure to await the LoadCustomer method

            ViewData["CusId"] = new SelectList(Customers, "CusId", "CusEmail");

            if (id == 0)
            {
                NotFound();
                return;
            }

            var ptId = HttpContext.Session.GetInt32("PtId");
            Notification = await _notificationService.GetByIdNotification(id);
            if (Notification == null)
            {
                NotFound();
                return;
            }

            if (ptId.HasValue)
            {
                Notification.PtId = ptId.Value; // Set the PtId from session
            }
        }

        private async Task LoadCustomer()
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
        public Notification Notification { get; set; } = new Notification(); // Initialize Notification

        public async Task<IActionResult> OnPostAsync()
        {
            
            await _notificationService.UpdateNotification(Notification);
            return RedirectToPage("./Index");
        }
    }
}
