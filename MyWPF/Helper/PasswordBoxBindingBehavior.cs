using System.Windows.Controls;
using System.Windows;
using MyWPF.ViewModel;

namespace MyWPF.Helper
{

    public static class PasswordBoxBindingBehavior
    {
        public static readonly DependencyProperty BoundPassword =
            DependencyProperty.RegisterAttached("BoundPassword", typeof(string), typeof(PasswordBoxBindingBehavior), new PropertyMetadata(string.Empty, OnBoundPasswordChanged));

        public static string GetBoundPassword(DependencyObject d)
        {
            var box = d as PasswordBox;
            return (box != null) ? (string)box.GetValue(BoundPassword) : string.Empty;
        }

        public static void SetBoundPassword(DependencyObject d, string value)
        {
            var box = d as PasswordBox;
            if (box != null)
            {
                box.PasswordChanged -= PasswordChanged;
                box.SetValue(BoundPassword, value);
                box.PasswordChanged += PasswordChanged;
            }
        }

        private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var box = d as PasswordBox;
            if (box != null)
            {
                box.PasswordChanged -= PasswordChanged;
                box.Password = (string)e.NewValue;
                box.PasswordChanged += PasswordChanged;
            }
        }

        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            var box = sender as PasswordBox;
            if (box != null)
            {
                SetBoundPassword(box, box.Password);
                if (box.DataContext is PersonalTrainerViewModel viewModel)
                {
                    viewModel.NewPersonalTrainer.PtPassword = box.Password;
                }
            }
        }

    }

}
