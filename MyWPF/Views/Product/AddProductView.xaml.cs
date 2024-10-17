using System.Windows;
using MyWPF.ViewModel;

namespace MyWPF.Views.Product
{
    public partial class AddProductView : Window
    { 
        public ProductViewModel productViewModel { get; set; }
        public CategoryViewModel categoryViewModel { get; set; }
        public AddProductView()
        {
            InitializeComponent();
            productViewModel = new ProductViewModel()
            {
                CloseAction = this.Close
            };
            categoryViewModel = new CategoryViewModel();
            this.DataContext = this;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }
    }
}
