using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using MyWPF.ViewModel;
using MyWPF.Views.Category;
using MyWPF.Views.Product;

namespace MyWPF.Views
{

    public partial class CategoryView : Page
    {
        public CategoryView()
        {
            InitializeComponent();
            DataContext = App.ServiceProvider.GetRequiredService<CategoryViewModel>();
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            var addCategoryWindow = new AddCategoryView();
            addCategoryWindow.ShowDialog();
        }
    }
}
