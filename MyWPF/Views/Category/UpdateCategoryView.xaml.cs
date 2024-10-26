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
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using MyWPF.ViewModel;

namespace MyWPF.Views.Category
{
    /// <summary>
    /// Interaction logic for UpdateCategoryView.xaml
    /// </summary>
    public partial class UpdateCategoryView : Window
    {
        public CategoryViewModel categoryViewModel { get; set; }
        public UpdateCategoryView(int cateId)
        {
            InitializeComponent();
            categoryViewModel = new CategoryViewModel()
            {
                CloseAction = this.Close
            };
            _ = categoryViewModel.LoadCategoryById(cateId);
            this.DataContext = this;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
