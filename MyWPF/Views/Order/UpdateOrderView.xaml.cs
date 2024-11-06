using System.Windows;
using MyWPF.ViewModel;

namespace MyWPF.Views.Order
{
    /// <summary>
    /// Interaction logic for UpdateOrderView.xaml
    /// </summary>
    public partial class UpdateOrderView : Window
    {
        private OrderViewModel _orderViewModel;
        public UpdateOrderView(int cusId)
        {
            InitializeComponent();
            _orderViewModel = new OrderViewModel()
            {
                CloseAction = this.Close
            };
            _ = _orderViewModel.LoadOrderById(cusId);
            DataContext = _orderViewModel;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
