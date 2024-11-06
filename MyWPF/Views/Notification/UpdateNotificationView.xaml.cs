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

namespace MyWPF.Views.Notification
{
    /// <summary>
    /// Interaction logic for UpdateNotificationView.xaml
    /// </summary>
    public partial class UpdateNotificationView : Window
    {
        public NotificationViewModel _notificationViewModel { get; set; }
        public UpdateNotificationView(int notiId)
        {
            InitializeComponent();
            _notificationViewModel = new NotificationViewModel()
            {
                CloseAction = this.Close
            };
            _ = _notificationViewModel.LoadNotificationById(notiId);
            DataContext = _notificationViewModel;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
