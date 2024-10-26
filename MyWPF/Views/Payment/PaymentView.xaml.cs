using System.Windows.Controls;
using MyWPF.ViewModel;

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
    }
}
