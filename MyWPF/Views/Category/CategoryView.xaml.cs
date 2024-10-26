using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using MyWPF.ViewModel;
using MyWPF.Views.Category;
using MyWPF.Views.Category;

namespace MyWPF.Views
{

    public partial class CategoryView : Page
    {
        public CategoryView()
        {
            InitializeComponent();
            DataContext = new CategoryViewModel();
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            var addCategoryWindow = new AddCategoryView();
            addCategoryWindow.ShowDialog();
        }

        private void UpdateCategory_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.CommandParameter != null)
            {
                int CateId = (int)button.CommandParameter;
                var updateCategoryView = new UpdateCategoryView(CateId);
                updateCategoryView.ShowDialog();
            }
        }
    }
}

