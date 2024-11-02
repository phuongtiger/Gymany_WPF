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

        public Customer _newCustomer { get; set; } = new Customer();

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

        public async Task LoadCustomerById(int cusId)
        {
            NewCustomer = await _customerService.GetByIdCustomer(cusId);

            if (NewCustomer == null)
            {
                MessageBox.Show($"PersonalTrainer with ID {cusId} not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (!string.IsNullOrEmpty(NewCustomer.CusImage))
            {
                ImageHelper.ImageSource = ImageHelper.LoadImage(NewCustomer.CusImage);
            }
        }

        private async Task<bool> CheckAccount(string username)
        {
            var existingCustomer = Customers.FirstOrDefault(c => c.CusUsername == username);
            if (existingCustomer != null)
            {
                MessageBox.Show("Username already exists. Please choose a different username.", "Duplicate Username", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        public Customer NewCustomer
        {
            get => _newCustomer;
            set
            {
                _newCustomer = value;
                OnPropertyChanged();
            }
        }

        public async void AddCustomer()
        {
            // Check for duplicate username
            if (!await CheckAccount(NewCustomer.CusUsername))
            {
                MessageBox.Show("Username already exists. Please choose another.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; // Prevent further execution
            }

            if (!string.IsNullOrEmpty(_newImagePath))
            {
                NewCustomer.CusImage = _newImagePath; // Apply the new image path when saving
            }

            try
            {
                await _customerService.AddCustomer(NewCustomer);
                Customers.Add(NewCustomer);
                NewCustomer = new Customer(); // Reset the form
                MessageBox.Show("Added customer successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                CloseAction?.Invoke(); // Close the AddCustomerView
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding the customer", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void UpdateCustomer()
        {
            try
            {
                if (!string.IsNullOrEmpty(_newImagePath))
                {
                    NewCustomer.CusImage = _newImagePath; // Apply the new image path when saving
                }

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
                    }
                }
                else
                {
                    MessageBox.Show("Customer not found.");
                }
                CloseAction?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding the customer", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
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
