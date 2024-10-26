using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MyWPF.ViewModel;
using MyWPF.Views.Customer;
using MyWPF.Views.Product;

namespace MyWPF.Views.Customer
{
    /// <summary>
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class CustomerView : Page
    {

        public CustomerView()
        {
            InitializeComponent();
            DataContext = new CustomerViewModel();
        }

        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            var addCustomerWindow = new AddCustomerView();
            addCustomerWindow.ShowDialog();
            //productViewModel.LoadProduct();
        }

        private void UpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.CommandParameter != null)
            {
                int prodId = (int)button.CommandParameter;
                var updateProductView = new UpdateProductView(prodId);
                updateProductView.ShowDialog();
            }
        }
    }
}
