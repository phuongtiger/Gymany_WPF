using System.Collections.ObjectModel;
using BussinessLogic.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Model;
using LiveCharts.Wpf;
using LiveCharts;
using System.Windows.Input;

namespace MyWPF.ViewModel
{
    public class OrderViewModel : BaseViewModel
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        public ObservableCollection<Order> Orders { get; set; } = new ObservableCollection<Order>();
        public Order _newOrder = new Order();
        public ICommand UpdateOrderCommand { get; private set; }
        public Action CloseAction { get; set; }

        public OrderViewModel()
        {
            _orderService = App.ServiceProvider.GetRequiredService<IOrderService>();
            _productService = App.ServiceProvider.GetRequiredService<IProductService>();
            _customerService = App.ServiceProvider.GetRequiredService<ICustomerService>();

            _ = LoadOrder();

            UpdateOrderCommand = new RelayCommand(UpdateOrder);
        }

        public async Task LoadOrder()
        {
            try
            {
                Orders.Clear();
                var orders = await _orderService.GetListAllOrder();
                foreach (var order in orders)
                {
                    order.Prod = await _productService.GetByIdProduct(order.ProdId);
                    order.Cus = await _customerService.GetByIdCustomer(order.CusId);
                    Orders.Add(order);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



        public async Task LoadOrderById(int prodId)
        {
            NewOrder = await _orderService.GetByIdOrder(prodId);

            if (NewOrder == null)
            {
                MessageBox.Show($"Order with ID {prodId} not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        public Order NewOrder
        {
            get => _newOrder;
            set
            {
                _newOrder = value;
                OnPropertyChanged();
            }
        }

        private async void UpdateOrder()
        {
            if (NewOrder != null)
            {
                var result = MessageBox.Show("Click 'YES' to accept order, 'No' to reject order?",
                                             "Confirm update!",
                                             MessageBoxButton.YesNoCancel,
                                             MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    NewOrder.OrderStatus = "Accepted";
                }
                else if (result == MessageBoxResult.No)
                {
                    NewOrder.OrderStatus = "Rejected";
                }
                else
                {
                    return;
                }
                await _orderService.UpdateOrder(NewOrder);
                MessageBox.Show($"Order status updated to {NewOrder.OrderStatus}.",
                                "Update Success",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                CloseAction?.Invoke();
            }
            else
            {
                // Nếu không tìm thấy đơn hàng, hiển thị thông báo lỗi
                MessageBox.Show("Order not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
