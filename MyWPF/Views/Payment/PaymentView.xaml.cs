using System.Windows;
using System.Windows.Controls;
using MyWPF.ViewModel;
using MyWPF.Views.Notification;

namespace MyWPF.Views.Payment
{
    public partial class PaymentView : Page
    {
        private readonly PaymentViewModel paymentViewModel;
        public PaymentView()
        {
            InitializeComponent();
            paymentViewModel = new PaymentViewModel();
            this.DataContext = paymentViewModel;
        }

        private void Detail_Click(object sender, RoutedEventArgs e)
        {
            var addNotificationWindow = new AddNotificationView();
            addNotificationWindow.ShowDialog();

        }
    }
}
