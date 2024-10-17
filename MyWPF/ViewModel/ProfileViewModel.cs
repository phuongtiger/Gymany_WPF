
using System.Windows;
using System.Windows.Input;
using BussinessLogic.Interface;
using BussinessLogic.Service;
using Microsoft.Extensions.DependencyInjection;
using Model;

namespace MyWPF.ViewModel
{
    public class ProfileViewModel : BaseViewModel
    {
        private readonly IAdminService _adminService;

        // Property to hold the selected admin
        private Model.Admin _selectedAdmin;
        public Model.Admin SelectedAdmin
        {
            get => _selectedAdmin;
            set
            {
                _selectedAdmin = value;
                OnPropertyChanged(nameof(SelectedAdmin)); // Notify the view of the change
            }
        }

        public ProfileViewModel()
        {
            _adminService = App.ServiceProvider.GetRequiredService<IAdminService>();
            LoadAdminByID();
            EditProfileCommand = new RelayCommandLogin(ExecuteEditProfile);
        }



        private async void LoadAdminByID()
        {
            // Check if the adminId exists in application properties
            if (Application.Current.Properties.Contains("adminId"))
            {
                // Get the adminId from application properties
                int adminId = (int)Application.Current.Properties["adminId"]; // Declare and assign adminId

                try
                {
                    // Attempt to get the admin details using the adminId
                    SelectedAdmin = await _adminService.GetByIdAdmin(adminId);    
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading admin: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Admin ID not found in application properties.");
            }
        }



        public ICommand EditProfileCommand { get; }

        private void ExecuteEditProfile(object parameter)
        {
            // Logic to edit the profile can be added here
            MessageBox.Show("Edit profile functionality is not yet implemented.");
        }
    }
}
