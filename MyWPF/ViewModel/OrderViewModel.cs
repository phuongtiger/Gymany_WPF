using System.Collections.ObjectModel;
using BussinessLogic.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Model;
using LiveCharts.Wpf;
using LiveCharts;

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
        //public ICommand UpdateOrderCommand { get; private set; }
        //public ICommand DeleteCommand { get; private set; }
        public Action CloseAction { get; set; }

        public OrderViewModel()
        {
            _orderService = App.ServiceProvider.GetRequiredService<IOrderService>();
            _productService = App.ServiceProvider.GetRequiredService<IProductService>();
            _customerService = App.ServiceProvider.GetRequiredService<ICustomerService>();

            _ = LoadOrder();
            
            //AddOrderCommand = new RelayCommand(AddOrder);
            //UpdateOrderCommand = new RelayCommand(UpdateOrder);
            //DeleteCommand = new RelayCommand<int>(DeleteOrder);
        }

        //public SeriesCollection SeriesCollection { get; set; }
        //public string[] Labels { get; set; }
        //public Func<int, string> Formatter { get; set; }

//        void Chart()
//{
//    SeriesCollection = new SeriesCollection();

//    // Assuming you have a valid list of orders
//    Dictionary<string, int> productQuantities = new Dictionary<string, int>();

//    foreach (var item in Orders)
//    {
//        if (item.Prod != null && item.OrderQuantity > 0) // Ensure valid data
//        {
//            // Add to the productQuantities dictionary or update the quantity if the product already exists
//            if (productQuantities.ContainsKey(item.Prod.ProdName))
//            {
//                productQuantities[item.Prod.ProdName] += item.OrderQuantity;
//            }
//            else
//            {
//                productQuantities[item.Prod.ProdName] = item.OrderQuantity;
//            }
//        }
//    }

//    // Display the content of the productQuantities dictionary in a MessageBox
//    string quantitiesDisplay = string.Join("\n", productQuantities.Select(kvp => $"{kvp.Key}: {kvp.Value}"));
//    MessageBox.Show(quantitiesDisplay, "Product Quantities");

//    // Add the series to the chart based on the productQuantities dictionary
//    foreach (var product in productQuantities)
//    {
//        SeriesCollection.Add(new ColumnSeries()
//        {
//            Title = product.Key,
//            Values = new ChartValues<int> { product.Value }
//        });
//    }

//    // Set the labels for the X-axis (product names)
//    Labels = productQuantities.Keys.ToArray();

//    // Optionally set the Formatter for better display (if needed for quantity)
//    Formatter = value => value.ToString("N0"); // Example for integer display
//}






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

        //private async void UpdateOrder()
        //{
        //    if (NewOrder != null)
        //    {
        //        var result = MessageBox.Show($"Do you want to update \"{NewOrder.OrderId}\"?",
        //                                     "Confirm update!",
        //                                     MessageBoxButton.YesNo,
        //                                     MessageBoxImage.Warning);
        //        if (result == MessageBoxResult.Yes)
        //        {
        //            await _orderService.UpdateOrder(NewOrder);
        //            var index = Orders.IndexOf(NewOrder);
        //            if (index >= 0)
        //            {
        //                Orders[index] = NewOrder;
        //            }
        //            LoadOrder();
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Order not found."); // Thông báo nếu không tìm thấy sản phẩm
        //    }
        //    CloseAction?.Invoke();
        //}

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
