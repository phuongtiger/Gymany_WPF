using System.Windows;
using MyWPF.ViewModel;

namespace MyWPF.Views.Category
{
    public partial class AddCategoryView : Window
    { 
        public CategoryViewModel categoryViewModel { get; set; }
        public AddCategoryView()
        {
            InitializeComponent();
            categoryViewModel = new CategoryViewModel()
            {
                CloseAction = this.Close
            };
            this.DataContext = this;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }
    }
}
