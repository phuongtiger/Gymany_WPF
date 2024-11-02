using System.Windows;
using System.Windows.Controls;
using MyWPF.ViewModel;
using MyWPF.Views.Product;

namespace MyWPF
{
    public partial class ProductView : Page
    {
        ProductViewModel productViewModel;
        public ProductView()
        {
            InitializeComponent();
            productViewModel = new ProductViewModel();
            DataContext = productViewModel;
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var addProductWindow = new AddProductView();
            addProductWindow.Closed += Window_Closed;
            addProductWindow.ShowDialog();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            productViewModel.LoadProduct();
        }

        private void UpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.CommandParameter != null)
            {
                int prodId = (int)button.CommandParameter;
                var updateProductView = new UpdateProductView(prodId);
                updateProductView.Closed += Window_Closed;
                updateProductView.ShowDialog();
            }
        }
    }
}
