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
        private readonly INotificationService _notificationService;

        public EditModel(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task OnGetAsync(int id)
        {
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

            ViewData["NotiType"] = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Value = "danger", Text = "Danger" },
                new SelectListItem { Value = "alert", Text = "Alert" }
            }, "Value", "Text");
        }


        [BindProperty]
        public Notification Notification { get; set; } = new Notification(); // Initialize Notification

		public async Task<IActionResult> OnPostAsync()
		{
			// Ensure the CusId remains unchanged during the update
			var existingNotification = await _notificationService.GetByIdNotification(Notification.NotiId);
			if (existingNotification == null)
			{
				return NotFound();
			}

			// Update only the properties you want to change
			existingNotification.NotiDate = Notification.NotiDate; 
			existingNotification.NotiContext = Notification.NotiContext; 
			existingNotification.NotiType = Notification.NotiType;
																 
			await _notificationService.UpdateNotification(existingNotification);
			return RedirectToPage("./Index");
		}
	}
}
