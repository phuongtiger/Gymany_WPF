using System.Windows;
using MyWPF.ViewModel;

namespace MyWPF.Views.PersonalTrainer
{

    public partial class UpdatePersonalTrainerView : Window
    {
        private PersonalTrainerViewModel _personalTrainerViewModel;
        public UpdatePersonalTrainerView(int ptId)
        {
            InitializeComponent();
            _personalTrainerViewModel = new PersonalTrainerViewModel()
            {
                CloseAction = this.Close
            };
            _ = _personalTrainerViewModel.LoadPersonalTrainerById(ptId);
            DataContext = _personalTrainerViewModel;
            
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
