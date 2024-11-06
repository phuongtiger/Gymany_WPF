using System.Windows;
using System.Windows.Input;
using MyWPF.ViewModel;
using MyWPF.Views;
using MyWPF.Views.Order;
using MyWPF.Views.PersonalTrainer;
using MyWPF.Views.Notification;
using MyWPF.Views.Customer;
using MyWPF.Views.Post;
using MyWPF.Views.Payment;
using BussinessLogic.Interface;
using BussinessLogic.Service;

namespace MyWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ProfileViewModel();
            frMain.Content = new ProfileView();
        }

        private bool IsMaximize = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximize)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximize = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximize = true;
                }
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Profile_Click(object sender, RoutedEventArgs e) => frMain.Content = new ProfileView();
        private void Product_Click(object sender, RoutedEventArgs e) => frMain.Content = new ProductView();
        private void Category_Click(object sender, RoutedEventArgs e) => frMain.Content = new CategoryView();
        private void Customer_Click(object sender, RoutedEventArgs e) => frMain.Content = new CustomerView();
        private void Order_Click(object sender, RoutedEventArgs e) => frMain.Content = new OrderView();
        private void PersonalTrainer_Click(object sender, RoutedEventArgs e) => frMain.Content = new PersonalTrainerView();
        private void Notification_Click(object sender, RoutedEventArgs e) => frMain.Content = new NotificationView();
        private void Post_Click(object sender, RoutedEventArgs e) => frMain.Content = new PostView();
        private void Payment_Click(object sender, RoutedEventArgs e) => frMain.Content = new PaymentView();

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            // Clear the adminId from application properties to log out the admin
            if (Application.Current.Properties.Contains("adminId"))
            {
                Application.Current.Properties.Remove("adminId");
            }

            MessageBox.Show("You have successfully logged out.");

            // Retrieve the IAdminService instance from the ServiceProvider
            var adminService = (IAdminService)App.ServiceProvider.GetService(typeof(IAdminService));

            // Open the LoginView and pass the adminService
            var loginView = new LoginView(adminService);
            loginView.Show();
            this.Close();
        }

    }

}