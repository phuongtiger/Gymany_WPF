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
using System.Windows.Shapes;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using MyWPF.ViewModel;

namespace MyWPF.Views.PersonalTrainer
{
    /// <summary>
    /// Interaction logic for UpdatePersonalTrainerView.xaml
    /// </summary>
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
