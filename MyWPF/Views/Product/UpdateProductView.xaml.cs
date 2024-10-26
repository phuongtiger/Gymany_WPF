using System.Windows;
using MyWPF.ViewModel;

namespace MyWPF.Views.Product
{
    public partial class UpdateProductView : Window
    {
        public ProductViewModel productViewModel { get; set; }
        public CategoryViewModel categoryViewModel { get; set; }
        public UpdateProductView(int prodId)
        {
            InitializeComponent();
            productViewModel = new ProductViewModel()
            {
                CloseAction = this.Close 
            };
            categoryViewModel = new CategoryViewModel();
            _ = productViewModel.LoadProductById(prodId);
            this.DataContext = this;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
