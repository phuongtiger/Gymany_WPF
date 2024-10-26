using System.Windows;
using MyWPF.ViewModel;

namespace MyWPF.Views.PersonalTrainer
{
    /// <summary>
    /// Interaction logic for AddPersonalTrainerView.xaml
    /// </summary>
    public partial class AddPersonalTrainerView : Window
    {
        private PersonalTrainerViewModel _personalTrainerViewModel;
        public AddPersonalTrainerView()
        {
            InitializeComponent();
            _personalTrainerViewModel = new PersonalTrainerViewModel()
            {
                CloseAction = this.Close
            };
            DataContext = _personalTrainerViewModel;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
