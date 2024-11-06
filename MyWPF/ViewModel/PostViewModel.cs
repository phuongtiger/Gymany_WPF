using System.Collections.ObjectModel;
using BussinessLogic.Interface;
using Microsoft.Extensions.DependencyInjection;
using Model;
using System.Windows.Input;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using BussinessLogic.Service;

namespace MyWPF.ViewModel
{
    public class PostViewModel : BaseViewModel
    {
        private readonly IPostService _postService;
        private readonly IPersonalTrainerService _personalTrainerService;
        private readonly ICustomerService _customerService;

        public ObservableCollection<Post> Posts { get; set; } = new ObservableCollection<Post>();
        public Post _newPost { get; set; } = new Post();
  
        public ICommand AddPostCommand { get; private set; }
        public ICommand UpdatePostCommand { get; private set; }
        public ICommand DeletePostCommand { get; private set; }
        public ImageHelper ImageHelper { get; set; }
        public ICommand BrowseImageCommand { get; }
        public Action CloseAction { get; set; }

        private readonly CustomerViewModel customerViewModel;
        public ObservableCollection<Customer> Customers => customerViewModel.Customers;
        private readonly PersonalTrainerViewModel personalTrainerViewModel;
        public ObservableCollection<PersonalTrainer> PersonalTrainers => personalTrainerViewModel.PersonalTrainers;
        public PostViewModel()
        {
            customerViewModel = new CustomerViewModel();
            customerViewModel.LoadCustomer();
            personalTrainerViewModel = new PersonalTrainerViewModel();
            personalTrainerViewModel.LoadPersonalTrainer();

            _postService = App.ServiceProvider.GetRequiredService<IPostService>();
            _personalTrainerService = App.ServiceProvider.GetRequiredService<IPersonalTrainerService>();
            _customerService = App.ServiceProvider.GetRequiredService<ICustomerService>();


            LoadPost();

            AddPostCommand = new RelayCommand(AddPost);
            UpdatePostCommand = new RelayCommand(UpdatePost);
            DeletePostCommand = new RelayCommand<int>(DeletePost);
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


        public async Task LoadPost()
        {
            try
            {
                Posts.Clear();
                var posts = await _postService.GetListAllPost();
                foreach (var post in posts)
                {             
                    Posts.Add(post);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task LoadPostById(int postId)
        {
            NewPost = await _postService.GetByIdPost(postId);

            if (NewPost == null)
            {
                MessageBox.Show($"Product with ID {postId} not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (!string.IsNullOrEmpty(NewPost.PostImg))
            {
                ImageHelper.ImageSource = ImageHelper.LoadImage(NewPost.PostImg);
            }
        }

        public Post NewPost
        {
            get => _newPost;
            set
            {
                _newPost = value;
                OnPropertyChanged();
            }
        }


        private async void AddPost()
        {
            try
            {
                if (Application.Current.Properties.Contains("adminId") && Application.Current.Properties["adminId"] != null)
                {
                    NewPost.AdminId = (int)Application.Current.Properties["adminId"];
                }

                // Set the current date and time
                NewPost.PostDate = DateTime.Now; // Assuming PostDate is of type DateTime
                NewPost.CusId = 1;
                NewPost.PtId = 1;

                if (!string.IsNullOrEmpty(_newImagePath))
                {
                    NewPost.PostImg = _newImagePath; // Apply the new image path when saving
                }

                await _postService.AddPost(NewPost);
                //Posts.Add(NewPost);
                MessageBox.Show("Added post successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                CloseAction?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding the post: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine(ex);
            }
        }


        private async void UpdatePost()
        {
            if (!string.IsNullOrEmpty(_newImagePath))
            {
                NewPost.PostImg = _newImagePath; // Apply the new image path when saving
            }
            if (NewPost != null)
            {
                var result = MessageBox.Show($"Do you want to update \"{NewPost.PostTitle}\"?",
                                             "Confirm update!",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    await _postService.UpdatePost(NewPost);
                    var index = Posts.IndexOf(NewPost);
                    if (index >= 0)
                    {
                        Posts[index] = NewPost;
                    }
                    LoadPost();
                }
            }
            else
            {
                MessageBox.Show("Post not found.");
            }
            CloseAction?.Invoke();
        }

        private async void DeletePost(int notiId)
        {
            if (NewPost != null)
            {
                var result = MessageBox.Show($"Do you want to delete \"{NewPost.PostTitle}\"?",
                                             "Confirm delete!",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    await _postService.DeletePost(notiId);
                    Posts.Remove(NewPost);
                    await LoadPost();
                }
            }
        }
    }
}
