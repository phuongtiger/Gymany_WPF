
using Model;
using BussinessLogic.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using BussinessLogic.Service;

namespace MyWPF.ViewModel
{
    public class PaymentViewModel : BaseViewModel
    {
        private readonly IPaymentService _paymentService;
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;
        public ObservableCollection<Payment> Payments { get; set; } = new ObservableCollection<Payment>();

        public Payment NewPayment { get; set; } = new Payment();

        public ICommand AddPaymentCommand { get; private set; }
        public ICommand UpdatePaymentCommand { get; private set; }
        public ICommand DeletePaymentCommand { get; private set; }

        private string _newImagePath;  // Temporary storage for the selected image path
        public ImageHelper ImageHelper { get; set; }
        public ICommand BrowseImageCommand { get; }
        public Action CloseAction { get; set; }

        private readonly CustomerViewModel customerViewModel;
        public ObservableCollection<Customer> Customers => customerViewModel.Customers;
        private readonly ProductViewModel productViewModel;
        public ObservableCollection<Product> Products => productViewModel.Products;

        public PaymentViewModel()
        {
            customerViewModel = new CustomerViewModel();
            customerViewModel.LoadCustomer();
            productViewModel = new ProductViewModel();
            productViewModel.LoadProduct();

            _paymentService = App.ServiceProvider.GetRequiredService<IPaymentService>();
            _productService = App.ServiceProvider.GetRequiredService<IProductService>();
            _customerService = App.ServiceProvider.GetRequiredService<ICustomerService>();

            _ = LoadPayment();

            AddPaymentCommand = new RelayCommand(AddPayment);
            ImageHelper = new ImageHelper();
            BrowseImageCommand = new RelayCommand(BrowseImage);
        }


        private void BrowseImage()
        {
            string selectedImagePath = ImageHelper.BrowseImage();

            if (!string.IsNullOrEmpty(selectedImagePath))
            {
                _newImagePath = selectedImagePath; // Temporarily store the new image path
                ImageHelper.ImageSource = ImageHelper.LoadImage(_newImagePath); // Show the selected image
            }
        }

        public async Task LoadPayment()
        {
            try
            {
                Payments.Clear();
                var payments = await _paymentService.GetListAllPayment();
                foreach (var payment in payments)
                {
                    payment.Cus = await _customerService.GetByIdCustomer(payment.CusId);
                    payment.Prod = await _productService.GetByIdProduct(payment.ProdId);

                    Payments.Add(payment);
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void AddPayment()
        {
            await _paymentService.AddPayment(NewPayment);
            Payments.Add(NewPayment);
            NewPayment = new Payment();
            MessageBox.Show("Added payment successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            await LoadPayment();
            CloseAction?.Invoke();
        }

        private async void UpdatePayment()
        {
            if (NewPayment != null)
            {
                var result = MessageBox.Show($"Do you want to update \"{NewPayment.PayId}\"?",
                                             "Confirm update!",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    await _paymentService.UpdatePayment(NewPayment);
                    var index = Payments.IndexOf(NewPayment);
                    if (index >= 0)
                    {
                        Payments[index] = NewPayment;
                    }
                    await LoadPayment();
                }
            }
            else
            {
                MessageBox.Show("Payment not found.");
            }
            CloseAction?.Invoke();
        }

        private async void DeletePayment(int notiId)
        {
            if (NewPayment != null)
            {
                var result = MessageBox.Show($"Do you want to delete \"{NewPayment.PayId}\"?",
                                             "Confirm delete!",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    await _paymentService.DeletePayment(notiId);
                    Payments.Remove(NewPayment);
                    await LoadPayment();
                }
            }
        }
    }
}
