using System.Windows;
using BussinessLogic.Interface;
using MyWPF.ViewModel;

namespace MyWPF.Views.Notification
{
 
    public partial class AddNotificationView : Window
    {
        private readonly NotificationViewModel notificationViewModel;

        public AddNotificationView()
        {
            InitializeComponent();
            notificationViewModel = new NotificationViewModel()
            {
                CloseAction = this.Close
            };
            DataContext = notificationViewModel;
            
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
