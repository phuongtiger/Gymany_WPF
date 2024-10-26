using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MyWPF.ViewModel;
using MyWPF.Views.Product;

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
            addPersonalTrainerWindow.ShowDialog();
            //productViewModel.LoadProduct();
        }

        private void UpdatePersonalTrainer_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.CommandParameter != null)
            {
                int ptId = (int)button.CommandParameter;
                var updatePersonalTrainerView = new UpdatePersonalTrainerView(ptId);
                updatePersonalTrainerView.ShowDialog();
            }
        }
    }
}
