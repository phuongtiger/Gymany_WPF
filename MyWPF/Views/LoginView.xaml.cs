using System.Windows;
using System.Windows.Controls;
using MyWPF.ViewModel;
using BussinessLogic.Interface; // Ensure this is included

namespace MyWPF.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView(IAdminService adminService) // Accept IAdminService
        {
            InitializeComponent();
            DataContext = new LoginViewModel(adminService); // Pass service to ViewModel
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            var viewModel = DataContext as LoginViewModel;
            if (viewModel != null)
            {
                viewModel.Password = passwordBox.Password; // Update the Password property
            }
        }
    }
}
