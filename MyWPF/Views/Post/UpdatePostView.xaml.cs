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

namespace MyWPF.Views.Post
{
    /// <summary>
    /// Interaction logic for UpdatePostUpdate.xaml
    /// </summary>
    public partial class UpdatePostView : Window
    {
        public PostViewModel postViewModel { get; set; }
        public UpdatePostView(int postId)
        {
            InitializeComponent();
            postViewModel = new PostViewModel()
            {
                CloseAction = this.Close
            };
            _ = postViewModel.LoadPostById(postId);
            this.DataContext = this;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
