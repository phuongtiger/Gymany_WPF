using System.Collections.ObjectModel;
using BussinessLogic.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Model;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace MyWPF.ViewModel
{
    public class CartViewModel : BaseViewModel
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        public ObservableCollection<Cart> Carts { get; set; } = new ObservableCollection<Cart>();
        public Cart _newCart = new Cart();
        public ICommand AddCartCommand { get; private set; }
        public ICommand UpdateCartCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public Action CloseAction { get; set; }

        public CartViewModel()
        {
            _cartService = App.ServiceProvider.GetRequiredService<ICartService>();
            _productService = App.ServiceProvider.GetRequiredService<IProductService>();
            _customerService = App.ServiceProvider.GetRequiredService<ICustomerService>();

            _ = LoadCart();

            AddCartCommand = new RelayCommand(AddCart);
            UpdateCartCommand = new RelayCommand(UpdateCart);
            DeleteCommand = new RelayCommand<int>(DeleteCart);
        }

        public async Task LoadCart()
        {
            try
            {
                Carts.Clear();
                var carts = await _cartService.GetListAllCart();
                foreach (var cart in carts)
                {
                    cart.Prod = await _productService.GetByIdProduct(cart.ProdId);
                    cart.Cus = await _customerService.GetByIdCustomer(cart.CusId);
                    Carts.Add(cart);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



        public async Task LoadCartById(int prodId)
        {
            NewCart = await _cartService.GetByIdCart(prodId);

            if (NewCart == null)
            {
                MessageBox.Show($"Cart with ID {prodId} not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        public Cart NewCart
        {
            get => _newCart;
            set
            {
                _newCart = value;
                OnPropertyChanged();
            }
        }

        private async void AddCart()
        {
            await _cartService.AddCart(NewCart);
            Carts.Add(NewCart);
            NewCart = new Cart();
            MessageBox.Show("Added cart success.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadCart();
            CloseAction?.Invoke();
        }

        private async void UpdateCart()
        {
            if (NewCart != null)
            {
                var result = MessageBox.Show($"Do you want to update \"{NewCart.CartId}\"?",
                                             "Confirm update!",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    await _cartService.UpdateCart(NewCart);
                    var index = Carts.IndexOf(NewCart);
                    if (index >= 0)
                    {
                        Carts[index] = NewCart;
                    }
                    LoadCart();
                }
            }
            else
            {
                MessageBox.Show("Cart not found."); // Thông báo nếu không tìm thấy sản phẩm
            }
            CloseAction?.Invoke();
        }

        private async void DeleteCart(int prodId)
        {
            if (NewCart != null)
            {
                var result = MessageBox.Show($"Do you want to delete \"{NewCart.CartId}\"?",
                                             "Confirm delete!",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    await _cartService.DeleteCart(prodId);
                    Carts.Remove(NewCart);
                    LoadCart();
                }
            }
        }
    }
}
