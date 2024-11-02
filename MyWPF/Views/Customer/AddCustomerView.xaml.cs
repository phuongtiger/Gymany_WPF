using System.Windows;
using MyWPF.ViewModel;

namespace MyWPF.Views.Customer
{
    public partial class AddCustomerView : Window
    {
        private CustomerViewModel _customerViewModel;
        public AddCustomerView()
        {
            InitializeComponent();
            _customerViewModel = new CustomerViewModel()
            {
                CloseAction = this.Close
            };
            DataContext = _customerViewModel;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}