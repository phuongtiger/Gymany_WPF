using System.Windows;
using MyWPF.ViewModel;

namespace MyWPF.Views.Post
{
    public partial class AddPostView : Window
    {
        private readonly PostViewModel postViewModel;
        public AddPostView()
        {
            InitializeComponent();
            postViewModel = new PostViewModel();
            postViewModel.CloseAction = new Action(this.Close); 
            DataContext = postViewModel;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}


