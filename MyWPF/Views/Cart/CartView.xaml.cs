using System.Windows.Controls;
using MyWPF.ViewModel;

namespace MyWPF.Views.Cart
{

    public partial class CartView : Page
    {
        private readonly CartViewModel cartViewModel;
        public CartView()
        {
            InitializeComponent();
            cartViewModel = new CartViewModel();
            this.DataContext = cartViewModel;
        }
    }
}
