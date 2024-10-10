using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using BussinessLogic.Interface;
using BussinessLogic.Service;
using Model;


namespace MyWPF.ViewModel
{
    public class ProductViewModel : BaseViewModel
    {
        private readonly IProductService _productService;
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();
        private Product _newProduct = new Product();
        private Product _selected;
        public ICommand AddCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ProductViewModel(IProductService productService)
        {
            Products = new ObservableCollection<Product>();
            _productService = productService;
            _ = LoadStudent();
            AddCommand = new RelayCommand(AddProduct);
            UpdateCommand = new RelayCommand(UpdateProduct);
            DeleteCommand = new RelayCommand(DeleteProduct);
        }

        public ProductViewModel() { }

        private async Task LoadStudent()
        {
            try
            {
                var products = await _productService.GetListAllProduct();
                Products.Clear();
                foreach (var product in products)
                {
                    Products.Add(product);
                }
            }
            catch (Exception ex)
            {
                // Ghi lại hoặc hiển thị lỗi
                Console.WriteLine(ex.Message);
            }
        }


        public Product Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                OnPropertyChanged(nameof(Selected));
                // When a student is selected, populate the fields for editing
                if (_selected != null)
                {
                    NewProduct = new Product
                    {
                        ProdId = _selected.ProdId,
                        ProdName = _selected.ProdName,
                        ProdDescription = _selected.ProdDescription,
                        ProdAmount = _selected.ProdAmount,
                        ProdImg = _selected.ProdImg,
                        ProdPrice = _selected.ProdPrice,
                        CateId = _selected.CateId
                    };
                }
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
            await _productService.AddProduct(NewProduct);
            Products.Add(NewProduct);
            NewProduct = new Product();
        }


        private async void UpdateProduct()
        {
            if (Selected != null)
            {
                await _productService.UpdateProduct(NewProduct);
                var index = Products.IndexOf(Selected);
                Products[index] = NewProduct;
            }
        }

        private async void DeleteProduct()
        {
            if (Selected != null)
            {
                await _productService.DeleteProduct(Selected.ProdId);
                Products.Remove(Selected);
                Selected = null;
            }
        }

    }
}
