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
using System.Windows.Shapes;
using MyWPF.ViewModel;

namespace MyWPF
{
    /// <summary>
    /// Interaction logic for AddProductView.xaml
    /// </summary>
    public partial class AddProductView : Window
    {
        public AddProductView(ProductViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel; // Set the DataContext to the view model
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Close the window without adding a product
        }
    }
}
