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

namespace PT_RazorPage.Pages.NotificationView
{
    public class IndexModel : PageModel
    {
        private readonly INotificationService _notificationService;

        public IndexModel(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public IList<Notification> Notification { get;set; } = default!;

        public async Task OnGetAsync()
        {
            // Retrieve the PtId from the session
            var ptId = HttpContext.Session.GetInt32("PtId");

            // Load workout plans associated with the PtId
            if (ptId.HasValue)
            {
                Notification = await _notificationService.GetListNotificationByPt(ptId.Value);
            }
            else
            {
                Notification = new List<Notification>(); // Handle the case where PtId is not set
            }
        }

    }
}
