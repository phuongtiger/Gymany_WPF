using System.Collections.ObjectModel;
using BussinessLogic.Interface;
using Microsoft.Extensions.DependencyInjection;
using Model;
using System.Windows.Input;
using System.Windows;
using GalaSoft.MvvmLight.Command;

namespace MyWPF.ViewModel
{
    public class CustomerViewModel : BaseViewModel
    {
        private readonly ICustomerService _customerService;
        public ObservableCollection<Customer> Customers { get; set; } = new ObservableCollection<Customer>();

        public Customer NewCustomer { get; set; } = new Customer();

        public ICommand AddCustomerCommand { get; private set; }
        public ICommand UpdateCustomerCommand { get; private set; }
        public ICommand DeleteCustomerCommand { get; private set; }

        private string _newImagePath;  // Temporary storage for the selected image path
        public ImageHelper ImageHelper { get; set; }
        public ICommand BrowseImageCommand { get; }
        public Action CloseAction { get; set; }

        public CustomerViewModel()
        {
            _customerService = App.ServiceProvider.GetRequiredService<ICustomerService>();

            _ = LoadCustomer();

            AddCustomerCommand = new RelayCommand(AddCustomer);
            UpdateCustomerCommand = new RelayCommand(UpdateCustomer);
            DeleteCustomerCommand = new RelayCommand<int>(DeleteCustomer);
            ImageHelper = new ImageHelper();
            BrowseImageCommand = new RelayCommand(BrowseImage);
        }


        private void BrowseImage()
        {
            string selectedImagePath = ImageHelper.BrowseImage();

            if (!string.IsNullOrEmpty(selectedImagePath))
            {
                _newImagePath = selectedImagePath; // Temporarily store the new image path
                ImageHelper.ImageSource = ImageHelper.LoadImage(_newImagePath); // Show the selected image
            }
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

        private async void AddCustomer()
        {
            if (string.IsNullOrWhiteSpace(NewCustomer.CusPassword))
            {
                MessageBox.Show("Password is required.");
                return;
            }
            await _customerService.AddCustomer(NewCustomer);
            Customers.Add(NewCustomer);
            NewCustomer = new Customer();
            MessageBox.Show("Added customer successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            await LoadCustomer();
            CloseAction?.Invoke();
        }

        private async void UpdateCustomer()
        {
            if (NewCustomer != null)
            {
                var result = MessageBox.Show($"Do you want to update \"{NewCustomer.CusName}\"?",
                                             "Confirm update!",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    await _customerService.UpdateCustomer(NewCustomer);
                    var index = Customers.IndexOf(NewCustomer);
                    if (index >= 0)
                    {
                        Customers[index] = NewCustomer;
                    }
                    await LoadCustomer();
                }
            }
            else
            {
                MessageBox.Show("Customer not found.");
            }
            CloseAction?.Invoke();
        }

        private async void DeleteCustomer(int notiId)
        {
            if (NewCustomer != null)
            {
                var result = MessageBox.Show($"Do you want to delete \"{NewCustomer.CusName}\"?",
                                             "Confirm delete!",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    await _customerService.DeleteCustomer(notiId);
                    Customers.Remove(NewCustomer);
                    await LoadCustomer();
                }
            }
        }
    }
}
