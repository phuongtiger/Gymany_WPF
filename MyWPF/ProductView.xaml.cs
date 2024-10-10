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

namespace MyWPF
{
    /// <summary>
    /// Interaction logic for ProductView.xaml
    /// </summary>
    public partial class ProductView : Page
    {
        private readonly ProductViewModel _productViewModel;

        // Constructor that accepts StudentViewModel
        public ProductView(ProductViewModel productViewModel)
        {
            InitializeComponent();
            _productViewModel = productViewModel;

            // Set the DataContext to the ViewModel for data binding
            DataContext = _productViewModel;
        }

        // Parameterless constructor (if needed)
        public ProductView()
        {
            InitializeComponent();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var addProductWindow = new AddProductView(this.DataContext as ProductViewModel);
            addProductWindow.ShowDialog();
        }
    }
}
