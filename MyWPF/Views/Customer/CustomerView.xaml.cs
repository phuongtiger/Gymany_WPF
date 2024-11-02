using System.Windows;
using System.Windows.Controls;
using MyWPF.ViewModel;

namespace MyWPF.Views.Customer
{
    public partial class CustomerView : Page
    {
        private CustomerViewModel customerViewModel;

        public CustomerView()
        {
            InitializeComponent();
            customerViewModel = new CustomerViewModel();
            DataContext = customerViewModel;
        }

        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            var addCustomerWindow = new AddCustomerView();
            addCustomerWindow.Closed += Window_Closed; // Subscribe to the Closed event
            addCustomerWindow.ShowDialog();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            // Refresh the customer list when the AddCustomerView is closed
            customerViewModel.LoadCustomer();
        }

        private void UpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.CommandParameter != null)
            {
                int cusId = (int)button.CommandParameter;
                var updateCustomerView = new UpdateCustomerView(cusId);
                updateCustomerView.Closed += Window_Closed; // Subscribe to the Closed event
                updateCustomerView.ShowDialog();
            }
        }
    }

}
