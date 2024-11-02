﻿using System;
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
using BussinessLogic.Service;

namespace PT_RazorPage.Pages.NotificationView
{
    public class CreateModel : PageModel
    {
        public ObservableCollection<Customer> Customers { get; set; }

        //    private readonly DataAccess.GymanyDbsContext _context;
        private readonly INotificationService _notificationService;
        private readonly ICustomerService _customerService;
        public CreateModel(INotificationService service,ICustomerService customerService)
        {
            _notificationService = service;
            _customerService = customerService;
            Customers = new ObservableCollection<Customer>();
            LoadCustomer();
        
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

        public IActionResult OnGet()
        {
            ViewData["CusId"] = new SelectList(Customers, "CusId", "CusEmail");

            Notification = new Notification
            {
                NotiDate = DateTime.Now, // hoặc DateTime.Today nếu chỉ cần ngày
            
            };

            var ptId = HttpContext.Session.GetInt32("PtId");
            if (ptId.HasValue)
            {
                Notification.PtId = ptId.Value; // Set the PtId from session
            }
            return Page();
        }

        [BindProperty]
        public Notification Notification { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            await _notificationService.AddNotification(Notification);

            return RedirectToPage("./Index");
        }
    }
}