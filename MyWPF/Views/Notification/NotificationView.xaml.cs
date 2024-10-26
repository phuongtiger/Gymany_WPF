using System.Windows;
using System.Windows.Controls;
using MyWPF.ViewModel;
using MyWPF.Views.Product;

namespace MyWPF.Views.Notification
{

    public partial class NotificationView : Page
    {
        private readonly NotificationViewModel _notificationViewModel;
        
        public NotificationView()
        {
            InitializeComponent();
            _notificationViewModel = new NotificationViewModel();
            this.DataContext = this._notificationViewModel;
        }

        private void AddNotification_Click(object sender, RoutedEventArgs e)
        {
            var addNotificationWindow = new AddNotificationView();
            addNotificationWindow.ShowDialog();
           
        }

        private void UpdateNotification_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.CommandParameter != null)
            {
                int prodId = (int)button.CommandParameter;
                var updateProductView = new UpdateProductView(prodId);
                updateProductView.ShowDialog();
            }
        }
    }
}
