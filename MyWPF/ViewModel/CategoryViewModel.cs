using System.Collections.ObjectModel;
using BussinessLogic.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Input;
using System.Windows;
using Model;
using GalaSoft.MvvmLight.CommandWpf;
using BussinessLogic.Service;


namespace MyWPF.ViewModel
{
    public class CategoryViewModel : BaseViewModel
    {
        private readonly ICategoryService _categoryService;
        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();
        private Category _newCategory = new Category();
        private Category _selected;
        public ICommand AddCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }
        public ICommand DeleteCommand { get; }
        public ImageHelper ImageHelper { get; set; }
        public ICommand BrowseImageCommand { get; }
        public Action CloseAction { get; set; }
        public CategoryViewModel()
        {
            _categoryService = App.ServiceProvider.GetRequiredService<ICategoryService>();
            _ = LoadCategory();
            AddCommand = new RelayCommand(AddCategory);
            UpdateCommand = new RelayCommand(UpdateCategory);
            DeleteCommand = new RelayCommand<int>(DeleteCategory);
            ImageHelper = new ImageHelper();
            BrowseImageCommand = new RelayCommand(BrowseImage);
        }

        private void BrowseImage()
        {
            string selectedImagePath = ImageHelper.BrowseImage();

            if (!string.IsNullOrEmpty(selectedImagePath))
            {
                NewCategory.CateImg = selectedImagePath;
            }
        }

        private async Task LoadCategory()
        {
            try
            {
                var categories = await _categoryService.GetListAllCategory();
                if (categories != null)
                {
                    Categories.Clear();
                    foreach (var category in categories)
                    {
                        Categories.Add(category);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading categories: {ex.Message}");
            }
        }

        public async Task LoadCategoryById(int prodId)
        {
            NewCategory = await _categoryService.GetByIdCategory(prodId);

            if (NewCategory == null)
            {
                MessageBox.Show($"Category with ID {prodId} not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (!string.IsNullOrEmpty(NewCategory.CateImg))
            {
                ImageHelper.ImageSource = ImageHelper.LoadImage(NewCategory.CateImg);
            }
        }
        public Category NewCategory
        {
            get => _newCategory;
            set
            {
                _newCategory = value;
                OnPropertyChanged();
            }
        }

        private async void AddCategory()
        {
            await _categoryService.AddCategory(NewCategory);
            Categories.Add(NewCategory);
            NewCategory = new Category();
            MessageBox.Show("Added category success.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            CloseAction?.Invoke();
        }


        private async void UpdateCategory()
        {
            if (NewCategory != null)
            {
                var result = MessageBox.Show($"Do you want to update \"{NewCategory.CateType}\"?",
                                             "Confirm update!",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    await _categoryService.UpdateCategory(NewCategory);
                    var index = Categories.IndexOf(NewCategory);
                    if (index >= 0)
                    {
                        Categories[index] = NewCategory;
                    }
                    LoadCategory();
                }
            }
            else
            {
                MessageBox.Show("Product not found."); // Thông báo nếu không tìm thấy sản phẩm
            }
            CloseAction?.Invoke();
        }

        private async void DeleteCategory(int cateId)
        {
            if (NewCategory != null)
            {
                var result = MessageBox.Show($"Do you want to delete \"{NewCategory.CateType}\"?",
                                             "Confirm delete!",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    await _categoryService.DeleteCategory(cateId);
                    Categories.Remove(NewCategory);
                    LoadCategory();
                }
            }
        }

    }
}
