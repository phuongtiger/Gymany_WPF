
using Model;
using BussinessLogic.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using GalaSoft.MvvmLight.Command;

namespace MyWPF.ViewModel
{
    public class PaymentViewModel : BaseViewModel
    {
        private readonly IPaymentService _paymentService;
        public ObservableCollection<Payment> Payments { get; set; } = new ObservableCollection<Payment>();

        public Payment NewPayment { get; set; } = new Payment();

        public ICommand AddPaymentCommand { get; private set; }
        public ICommand UpdatePaymentCommand { get; private set; }
        public ICommand DeletePaymentCommand { get; private set; }

        private string _newImagePath;  // Temporary storage for the selected image path
        public ImageHelper ImageHelper { get; set; }
        public ICommand BrowseImageCommand { get; }
        public Action CloseAction { get; set; }

        public PaymentViewModel()
        {
            _paymentService = App.ServiceProvider.GetRequiredService<IPaymentService>();

            _ = LoadPayment();

            AddPaymentCommand = new RelayCommand(AddPayment);
            UpdatePaymentCommand = new RelayCommand(UpdatePayment);
            DeletePaymentCommand = new RelayCommand<int>(DeletePayment);
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
