using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using BussinessLogic.Interface;
using GalaSoft.MvvmLight.Command;
using MailChimp.Net.Models;
using Microsoft.Extensions.DependencyInjection;
using Model;

namespace MyWPF.ViewModel
{
    public class PersonalTrainerViewModel : BaseViewModel
    {
        private readonly IPersonalTrainerService _personalTrainerService;
        public ObservableCollection<PersonalTrainer> PersonalTrainers { get; set; } = new ObservableCollection<PersonalTrainer>();
        public PersonalTrainer _newPersonalTrainer = new PersonalTrainer();
        
        public ICommand AddPersonalTrainerCommand { get; private set; }
        public ICommand UpdatePersonalTrainerCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        private string _newImagePath;  // Temporary storage for the selected image path
        public ImageHelper ImageHelper { get; set; }
        public ICommand BrowseImageCommand { get; }
        public Action CloseAction { get; set; }

        public PersonalTrainerViewModel()
        {
            _personalTrainerService = App.ServiceProvider.GetRequiredService<IPersonalTrainerService>();
            _ = LoadPersonalTrainer();
            AddPersonalTrainerCommand = new RelayCommand(AddPersonalTrainer);
            UpdatePersonalTrainerCommand = new RelayCommand(UpdatePersonalTrainer);
            DeleteCommand = new RelayCommand<int>(DeletePersonalTrainer);
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

        public async Task LoadPersonalTrainer()
        {
            try
            {
                PersonalTrainers.Clear();
                var personalTrainers = await _personalTrainerService.GetListAllPersonalTrainer();
                foreach (var personalTrainer in personalTrainers)
                {
                    PersonalTrainers.Add(personalTrainer);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task LoadPersonalTrainerById(int ptId)
        {
            NewPersonalTrainer = await _personalTrainerService.GetByIdPersonalTrainer(ptId);

            if (NewPersonalTrainer == null)
            {
                MessageBox.Show($"PersonalTrainer with ID {ptId} not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (!string.IsNullOrEmpty(NewPersonalTrainer.PtImg))
            {
                ImageHelper.ImageSource = ImageHelper.LoadImage(NewPersonalTrainer.PtImg);
            }
        }

        public PersonalTrainer NewPersonalTrainer
        {
            get => _newPersonalTrainer;
            set
            {
                _newPersonalTrainer = value;
                OnPropertyChanged();
            }
        }


        private async Task<bool> CheckAccount(string username)
        {
            var existingCustomer = PersonalTrainers.FirstOrDefault(c => c.PtUsername == username);
            if (existingCustomer != null)
            {
                MessageBox.Show("Username already exists. Please choose a different username.", "Duplicate Username", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        private async void AddPersonalTrainer()
        {
            if (!await CheckAccount(NewPersonalTrainer.PtName))
            {
                return;
            }
            else
            {
                if (!string.IsNullOrEmpty(_newImagePath))
                {
                    NewPersonalTrainer.PtImg = _newImagePath; // Apply the new image path when saving
                }
                try
                {
                    await _personalTrainerService.AddPersonalTrainer(NewPersonalTrainer);
                    PersonalTrainers.Add(NewPersonalTrainer);
                    NewPersonalTrainer = new PersonalTrainer();
                    MessageBox.Show("Added personalTrainer success.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    await LoadPersonalTrainer();
                    CloseAction?.Invoke();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while adding the customer", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }         
        }

        private async void UpdatePersonalTrainer()
        {
            if (!string.IsNullOrEmpty(_newImagePath))
            {
                NewPersonalTrainer.PtImg = _newImagePath; // Apply the new image path when saving
            }

            if (NewPersonalTrainer != null)
            {
                var result = MessageBox.Show($"Do you want to update \"{NewPersonalTrainer.PtName}\"?",
                                             "Confirm update!",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    await _personalTrainerService.UpdatePersonalTrainer(NewPersonalTrainer);
                    var index = PersonalTrainers.IndexOf(NewPersonalTrainer);
                    if (index >= 0)
                    {
                        PersonalTrainers[index] = NewPersonalTrainer;
                    }
                }
            }
            else
            {
                MessageBox.Show("PersonalTrainer not found.");
            }
            CloseAction?.Invoke();
        }

        private async void DeletePersonalTrainer(int ptId)
        {
            if (NewPersonalTrainer != null)
            {
                var result = MessageBox.Show($"Do you want to delete \"{NewPersonalTrainer.PtName}\"?",
                                             "Confirm delete!",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    await _personalTrainerService.DeletePersonalTrainer(ptId);
                    PersonalTrainers.Remove(NewPersonalTrainer);
                    LoadPersonalTrainer();
                }
            }
        }
    }
}
