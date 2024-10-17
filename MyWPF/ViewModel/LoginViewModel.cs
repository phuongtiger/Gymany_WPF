using System.Windows.Input;

using System.Windows;
using BussinessLogic.Interface;
using System.Collections.ObjectModel;
using Model;
using MyWPF.Views;

namespace MyWPF.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAdminService _adminService;
        public ObservableCollection<Admin> Admins { get; set; } = new ObservableCollection<Admin>();

        // Constructor
        public LoginViewModel(IAdminService adminService)
        {
            _adminService = adminService; // Inject the service
            LoadAdmin(); // Load the admins asynchronously
            LoginCommand = new RelayCommandLogin(ExecuteLogin, CanExecuteLogin);
        }

        // Load all admins
        private async Task LoadAdmin()
        {
            try
            {
                var admins = await _adminService.GetListAllAdmin();
                Admins.Clear();
                foreach (var admin in admins)
                {
                    Admins.Add(admin);
                }
            }
            catch (Exception ex)
            {
                // Log or display the error
                Console.WriteLine(ex.Message);
            }
        }

        // Properties for username and password
        private string _username;
        private string _password;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
                ((RelayCommandLogin)LoginCommand).RaiseCanExecuteChanged(); // Notify command
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
                ((RelayCommandLogin)LoginCommand).RaiseCanExecuteChanged(); // Notify command
            }
        }

        // Login command
        public ICommand LoginCommand { get; }

        // Determine if login can be executed
        private bool CanExecuteLogin(object parameter)
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }

        // Execute login
        private void ExecuteLogin(object parameter)
        {
            // Validate against the loaded admins
            var admin = Admins.FirstOrDefault(a => a.AdminUsername == Username && a.AdminPassword == Password);
            if (admin != null)
            {
                // Set the adminId in Application.Current.Properties
                Application.Current.Properties["adminId"] = admin.AdminId; // Assuming AdminId is the property for the ID
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                // Close the LoginView
                Application.Current.Windows.OfType<LoginView>().FirstOrDefault()?.Close();
            }
            else
            {
                MessageBox.Show("Login Failed. Invalid credentials.");
            }
        }

    }
}
