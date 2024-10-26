using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using BussinessLogic.Interface;
using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.DependencyInjection;
using Model;


namespace MyWPF.ViewModel
{
    public class ProductViewModel : BaseViewModel
    {
        private readonly IProductService _productService;
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();
        public Product _newProduct = new Product();
        public ICommand AddProductCommand { get; private set; }
        public ICommand UpdateProductCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ImageHelper ImageHelper { get; set; }
        public ICommand BrowseImageCommand { get; }
        public Action CloseAction { get; set; } 

        public ProductViewModel()
        {
            _productService = App.ServiceProvider.GetRequiredService<IProductService>();
            _ = LoadProduct();
            AddProductCommand = new RelayCommand(AddProduct);
            UpdateProductCommand = new RelayCommand(UpdateProduct);
            DeleteCommand = new RelayCommand<int>(DeleteProduct);
            ImageHelper = new ImageHelper();
            BrowseImageCommand = new RelayCommand(BrowseImage);
        }

        private string _newImagePath;
        private void BrowseImage()
        {
            string selectedImagePath = ImageHelper.BrowseImage();

            if (!string.IsNullOrEmpty(selectedImagePath))
            {
                _newImagePath = selectedImagePath; // Temporarily store the new image path
                ImageHelper.ImageSource = ImageHelper.LoadImage(_newImagePath); // Show the selected image
            }
        }


        public async Task LoadProduct()
        {
            try
            {
                Products.Clear();
                var products = await _productService.GetListAllProduct();
                foreach (var product in products)
                {
                    Products.Add(product);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task LoadProductById(int prodId)
        {
            NewProduct = await _productService.GetByIdProduct(prodId);

            if (NewProduct == null)
            {
                MessageBox.Show($"Product with ID {prodId} not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (!string.IsNullOrEmpty(NewProduct.ProdImg))
            {
                ImageHelper.ImageSource = ImageHelper.LoadImage(NewProduct.ProdImg);
            }
        }


        public Product NewProduct
        {
            get => _newProduct;
            set
            {
                _newProduct = value;
                OnPropertyChanged(); 
            }
        }

        private async void AddProduct()
        {
            if (!string.IsNullOrEmpty(_newImagePath))
            {
                NewProduct.ProdImg = _newImagePath; // Apply the new image path when saving
            }

            await _productService.AddProduct(NewProduct);
            Products.Add(NewProduct);
            NewProduct = new Product();
            MessageBox.Show("Added product success.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadProduct();
            CloseAction?.Invoke();
        }

        private async void UpdateProduct()
        {
            if (!string.IsNullOrEmpty(_newImagePath))
            {
                NewProduct.ProdImg = _newImagePath; // Apply the new image path when saving
            }

            if (NewProduct != null)
            {
                var result = MessageBox.Show($"Do you want to update \"{NewProduct.ProdName}\"?",
                                             "Confirm update!",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    await _productService.UpdateProduct(NewProduct);
                    var index = Products.IndexOf(NewProduct);
                    if (index >= 0)
                    {
                        Products[index] = NewProduct;
                    }
                    LoadProduct();
                }
            }
            else
            {
                MessageBox.Show("Product not found.");
            }
            CloseAction?.Invoke();
        }


        private async void DeleteProduct(int prodId)
        {
            if (NewProduct != null)
            {
                var result = MessageBox.Show($"Do you want to delete \"{NewProduct.ProdName}\"?",
                                             "Confirm delete!",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    await _productService.DeleteProduct(prodId);
                    Products.Remove(NewProduct);
                    LoadProduct();
                }
            }
        }
    }
}
