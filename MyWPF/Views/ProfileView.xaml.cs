using System.Windows.Controls;
using BussinessLogic.Interface;
using MyWPF.ViewModel;


namespace MyWPF.Views
{
    /// <summary>
    /// Interaction logic for ProfileView.xaml
    /// </summary>
    public partial class ProfileView : Page
    {
        public ProfileView()
        {
            InitializeComponent();
            DataContext = new ProfileViewModel();
        }

    }
}
