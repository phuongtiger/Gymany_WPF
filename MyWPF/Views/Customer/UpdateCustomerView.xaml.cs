using System.Windows;
using MyWPF.ViewModel;
namespace MyWPF.Views.Customer
{

    public partial class UpdateCustomerView : Window
    {

        private CustomerViewModel _customerViewModel;
        public UpdateCustomerView(int cusId)
        {
            InitializeComponent();
            _customerViewModel = new CustomerViewModel()
            {
                CloseAction = this.Close
            };
            _ = _customerViewModel.LoadCustomerById(cusId);
            DataContext = _customerViewModel;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
