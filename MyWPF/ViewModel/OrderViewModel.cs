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
        //public ICommand AddOrderCommand { get; private set; }
        public ICommand UpdateOrderCommand { get; private set; }
        public ICommand DeleteOrderCommand { get; private set; }
        public Action CloseAction { get; set; }

        public OrderViewModel()
        {
            _orderService = App.ServiceProvider.GetRequiredService<IOrderService>();
            _productService = App.ServiceProvider.GetRequiredService<IProductService>();
            _customerService = App.ServiceProvider.GetRequiredService<ICustomerService>();

            _ = LoadOrder();

            //AddOrderCommand = new RelayCommand(AddOrder);
            UpdateOrderCommand = new RelayCommand(UpdateOrder);
            DeleteOrderCommand = new RelayCommand(DeleteOrder);
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

        //private async void AddOrder()
        //{
        //    await _orderService.AddOrder(NewOrder);
        //    Orders.Add(NewOrder);
        //    NewOrder = new Order();
        //    MessageBox.Show("Added order success.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        //    LoadOrder();
        //    CloseAction?.Invoke();
        //}

        private async void UpdateOrder()
        {
            if (NewOrder != null)
            {
                var result = MessageBox.Show($"Do you want to update status to PENDING",
                                             "Confirm update!",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    
                    if (NewOrder.OrderStatus == "Waiting")
                    {
                        NewOrder.OrderStatus = "Pending"; 
                    }

                    await _orderService.UpdateOrder(NewOrder);

                    var index = Orders.IndexOf(Orders.FirstOrDefault(o => o.OrderId == NewOrder.OrderId));
                    if (index >= 0)
                    {
                        // Update properties of the existing order instead of replacing it
                        var existingOrder = Orders[index];
                        existingOrder.OrderStatus = NewOrder.OrderStatus; // Ensure these properties trigger notifications
                        existingOrder.OrderTotalPrice = NewOrder.OrderTotalPrice;
                        existingOrder.OrderQuantity = NewOrder.OrderQuantity;
                        existingOrder.OrderStartDate = NewOrder.OrderStartDate;
                    }

                    await LoadOrder(); 
                }
            }
            else
            {
                MessageBox.Show("Order not found.");
            }
        }


        private async void DeleteOrder()
        {
            if (NewOrder != null)
            {
                var result = MessageBox.Show($"Do you want to update status to REJECTED",
                                             "Confirm update!",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    // Change status from "Waiting" to "Pending"
                    if (NewOrder.OrderStatus == "Waiting")
                    {
                        NewOrder.OrderStatus = "Rejected"; // Update the status
                    }

                    await _orderService.UpdateOrder(NewOrder);

                    var index = Orders.IndexOf(Orders.FirstOrDefault(o => o.OrderId == NewOrder.OrderId));
                    if (index >= 0)
                    {
     
                        var existingOrder = Orders[index];
                        existingOrder.OrderStatus = NewOrder.OrderStatus;
                        existingOrder.OrderTotalPrice = NewOrder.OrderTotalPrice;
                        existingOrder.OrderQuantity = NewOrder.OrderQuantity;
                        existingOrder.OrderStartDate = NewOrder.OrderStartDate;
                    }

                    await LoadOrder(); // Await the method to ensure it completes
                }
            }
            else
            {
                MessageBox.Show("Order not found."); // Inform if the order is not found
            }
        }



        

        //private async void DeleteOrder(int prodId)
        //{
        //    if (NewOrder != null)
        //    {
        //        var result = MessageBox.Show($"Do you want to delete \"{NewOrder.OrderId}\"?",
        //                                     "Confirm delete!",
        //                                     MessageBoxButton.YesNo,
        //                                     MessageBoxImage.Warning);
        //        if (result == MessageBoxResult.Yes)
        //        {
        //            await _orderService.DeleteOrder(prodId);
        //            Orders.Remove(NewOrder);
        //            LoadOrder();
        //        }
        //    }
        //}
    }
}
