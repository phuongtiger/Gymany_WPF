using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;
using MyWPF.ViewModel;

namespace MyWPF.Views.Order
{
    public partial class OrderView : Page
    {
        public OrderViewModel orderViewModel { get; set; }
        public OrderView()
        {
            InitializeComponent();
            orderViewModel = new OrderViewModel();
            this.DataContext = orderViewModel;
        }

        
    }
}
