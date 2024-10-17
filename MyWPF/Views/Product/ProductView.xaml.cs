using System.Windows;
using System.Windows.Controls;
using MyWPF.ViewModel;
using MyWPF.Views.Product;

namespace MyWPF
{
    /// <summary>
    /// Interaction logic for ProductView.xaml
    /// </summary>
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
            addProductWindow.ShowDialog();
            //productViewModel.LoadProduct();
        }

        private void UpdateProduct_Click(object sender, RoutedEventArgs e)
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
