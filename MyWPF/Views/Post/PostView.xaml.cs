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
using MyWPF.Views.Post;
using MyWPF.Views.Post;
using MyWPF.Views.Post;

namespace MyWPF.Views.Post
{
    /// <summary>
    /// Interaction logic for PostView.xaml
    /// </summary>
    public partial class PostView : Page
    {
        private readonly PostViewModel _postViewModel;

        public PostView()
        {
            InitializeComponent();
            _postViewModel = new PostViewModel();
            this.DataContext = this._postViewModel;
        }

        private void AddPost_Click(object sender, RoutedEventArgs e)
        {
            var addPostWindow = new AddPostView();
            addPostWindow.Closed += Window_Closed;
            addPostWindow.ShowDialog();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _postViewModel.LoadPost();
        }

        private void UpdatePost_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.CommandParameter != null)
            {
                int PostId = (int)button.CommandParameter;
                var updatePostView = new UpdatePostView(PostId);
                updatePostView.Closed += Window_Closed;
                updatePostView.ShowDialog();
            }
        }
    }
}
