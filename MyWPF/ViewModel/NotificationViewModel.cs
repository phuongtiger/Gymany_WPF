﻿using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using BussinessLogic.Interface;
using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.DependencyInjection;
using Model;

namespace MyWPF.ViewModel
{
    public class NotificationViewModel : BaseViewModel
    {
        private readonly INotificationService _notificationService;
        private readonly IPersonalTrainerService _personalTrainerService;
        private readonly ICustomerService _customerService;

        public ObservableCollection<Notification> Notifications { get; set; } = new ObservableCollection<Notification>();
        public Notification NewNotification { get; set; } = new Notification();
        public ObservableCollection<string> NotificationTypes { get; set; }

        public ICommand AddNotificationCommand { get; private set; }
        public ICommand UpdateNotificationCommand { get; private set; }
        public ICommand DeleteNotificationCommand { get; private set; }
        public Action CloseAction { get; set; }

        private readonly CustomerViewModel customerViewModel;
        public ObservableCollection<Customer> Customers => customerViewModel.Customers;
        private readonly PersonalTrainerViewModel personalTrainerViewModel;
        public ObservableCollection<PersonalTrainer> PersonalTrainers => personalTrainerViewModel.PersonalTrainers;
        public NotificationViewModel()
        {
            customerViewModel = new CustomerViewModel();
            customerViewModel.LoadCustomer();
            personalTrainerViewModel = new PersonalTrainerViewModel();
            personalTrainerViewModel.LoadPersonalTrainer();

            _notificationService = App.ServiceProvider.GetRequiredService<INotificationService>();
            _personalTrainerService = App.ServiceProvider.GetRequiredService<IPersonalTrainerService>();
            _customerService = App.ServiceProvider.GetRequiredService<ICustomerService>();

            NotificationTypes = new ObservableCollection<string> { "Warning", "Alert", "Info" };

            LoadNotification();

            AddNotificationCommand = new RelayCommand(AddNotification);
            UpdateNotificationCommand = new RelayCommand(UpdateNotification);
            DeleteNotificationCommand = new RelayCommand<int>(DeleteNotification);
        }

          
        private async Task LoadNotification()
        {
            try
            {
                Notifications.Clear();
                var notifications = await _notificationService.GetListAllNotification();
                foreach (var notification in notifications)
                {
                    notification.Pt = await _personalTrainerService.GetByIdPersonalTrainer(notification.PtId);
                    notification.Cus = await _customerService.GetByIdCustomer(notification.CusId);
                    Notifications.Add(notification);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        } 

        private async void AddNotification()
        {
            await _notificationService.AddNotification(NewNotification);
            Notifications.Add(NewNotification);
            NewNotification = new Notification();
            MessageBox.Show("Added notification successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            await LoadNotification();
            CloseAction?.Invoke();
        }

        private async void UpdateNotification()
        {
            if (NewNotification != null)
            {
                var result = MessageBox.Show($"Do you want to update \"{NewNotification.NotiType}\"?",
                                             "Confirm update!",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    await _notificationService.UpdateNotification(NewNotification);
                    var index = Notifications.IndexOf(NewNotification);
                    if (index >= 0)
                    {
                        Notifications[index] = NewNotification;
                    }
                    await LoadNotification();
                }
            }
            else
            {
                MessageBox.Show("Notification not found.");
            }
            CloseAction?.Invoke();
        }

        private async void DeleteNotification(int notiId)
        {
            if (NewNotification != null)
            {
                var result = MessageBox.Show($"Do you want to delete \"{NewNotification.NotiType}\"?",
                                             "Confirm delete!",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    await _notificationService.DeleteNotification(notiId);
                    Notifications.Remove(NewNotification);
                    await LoadNotification();
                }
            }
        }
    }

}