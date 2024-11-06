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

        //private void AddNotification_Click(object sender, RoutedEventArgs e)
        //{
        //    var addNotificationWindow = new AddNotificationView();
        //    addNotificationWindow.Closed += Window_Closed;
        //    addNotificationWindow.ShowDialog();
           
        //}

        private void Window_Closed(object sender, EventArgs e)
        {
            _notificationViewModel.LoadNotification();
        }

        private void UpdateNotification_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.CommandParameter != null)
            {
                int notiId = (int)button.CommandParameter;
                var updateNotificationView = new UpdateNotificationView(notiId);
                updateNotificationView.ShowDialog();
            }
        }
    }
}
