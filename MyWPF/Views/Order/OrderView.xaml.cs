using System.Windows;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;
using MyWPF.ViewModel;
using MyWPF.Views.Customer;

namespace MyWPF.Views.Order
{
    public partial class OrderView : Page
    {
        public OrderViewModel orderViewModel { get; set; }
        public OrderView()
        {
            InitializeComponent();
            orderViewModel = new OrderViewModel();
            this.DataContext = orderViewModel;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            // Refresh the customer list when the AddCustomerView is closed
            orderViewModel.LoadOrder();
        }

        private void UpdateOrder_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.CommandParameter != null)
            {
                int orderId = (int)button.CommandParameter;
                var updateCustomerView = new UpdateOrderView(orderId);
                updateCustomerView.Closed += Window_Closed; // Subscribe to the Closed event
                updateCustomerView.ShowDialog();
            }
        }

    }
}
