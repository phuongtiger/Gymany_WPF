using System.Windows;
using System.Windows.Controls;
using MyWPF.ViewModel;


namespace MyWPF.Views.PersonalTrainer
{
    /// <summary>
    /// Interaction logic for PersonalTrainerView.xaml
    /// </summary>
    public partial class PersonalTrainerView : Page
    {
        private PersonalTrainerViewModel _personalTrainerViewModel;
        public PersonalTrainerView()
        {
            InitializeComponent();
            _personalTrainerViewModel = new PersonalTrainerViewModel();
            DataContext = _personalTrainerViewModel;
        }

        private void AddPersonalTrainer_Click(object sender, RoutedEventArgs e)
        {
            var addPersonalTrainerWindow = new AddPersonalTrainerView();
            addPersonalTrainerWindow.Closed += Window_Closed; // Subscribe to the Closed event
            addPersonalTrainerWindow.ShowDialog();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            _personalTrainerViewModel.LoadPersonalTrainer();
        }

        private void UpdatePersonalTrainer_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.CommandParameter != null)
            {
                int ptId = (int)button.CommandParameter;
                var updatePersonalTrainerView = new UpdatePersonalTrainerView(ptId);
                updatePersonalTrainerView.Closed += Window_Closed;
                updatePersonalTrainerView.ShowDialog();
            }
        }
    }
}
