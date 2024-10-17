using System.Windows;
using System.Windows.Input;
using MyWPF.ViewModel;
using MyWPF.Views;
using MyWPF.Views.Order;

namespace MyWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ProfileViewModel();
            frMain.Content = new ProfileView();
        }

        private bool IsMaximize = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximize)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximize = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximize = true;
                }
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Profile_Click(object sender, RoutedEventArgs e) => frMain.Content = new ProfileView();

        private void Product_Click(object sender, RoutedEventArgs e) => frMain.Content = new ProductView();

        private void Category_Click(object sender, RoutedEventArgs e) => frMain.Content = new CategoryView();

        private void Order_Click(object sender, RoutedEventArgs e) => frMain.Content = new OrderView();


    }

}