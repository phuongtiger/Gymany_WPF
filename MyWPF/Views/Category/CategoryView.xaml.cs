using System.Windows;
using System.Windows.Controls;
using MyWPF.ViewModel;
using MyWPF.Views.Category;


namespace MyWPF.Views
{
    public partial class CategoryView : Page
    {
        private readonly CategoryViewModel _categoryViewModel;
        public CategoryView()
        {
            InitializeComponent();
            _categoryViewModel= new CategoryViewModel();
            DataContext = _categoryViewModel;
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            var addCategoryWindow = new AddCategoryView();
            addCategoryWindow.Closed += Window_Closed;
            addCategoryWindow.ShowDialog();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            _categoryViewModel.LoadCategory();
        }

        private void UpdateCategory_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.CommandParameter != null)
            {
                int CateId = (int)button.CommandParameter;
                var updateCategoryView = new UpdateCategoryView(CateId);
                updateCategoryView.Closed += Window_Closed;
                updateCategoryView.ShowDialog();
            }
        }
    }
}

