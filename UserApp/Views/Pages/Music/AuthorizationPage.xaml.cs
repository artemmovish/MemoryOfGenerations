using Entity.Models;
using Infastructure.Services;
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
using UserApp.ViewModels.Base;

namespace UserApp.Views.Pages.Music
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private async void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            var user = new User()
            {
                Username = Username2.Text,
                Password = Password2.Text
            };

            await UserService.AddUserAsync(user);

            DataStore.Instance.User = await UserService.AuthenticateAsync(user.Username, user.Password);
            DataStore.MainViewModel.Message = "Вы зарегестрировались";

            DataStore.NavigationService.GoBack();
            DataStore.NavigationService.Navigate(DataStore.Instance.MainBookPage);
        }

        private void HyperlinkToReg_Click(object sender, RoutedEventArgs e)
        {
            RegistrationPanel.Visibility = Visibility.Collapsed;
            RegistrationPanel2.Visibility = Visibility.Visible;
        }

        private void HyperlinkToAuth_Click(object sender, RoutedEventArgs e)
        {
            RegistrationPanel.Visibility = Visibility.Visible;
            RegistrationPanel2.Visibility = Visibility.Collapsed;
        }

        private async void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            var user = await UserService.AuthenticateAsync(Username.Text, Password.Text);


            if (user != null)
            {
                DataStore.Instance.User = user;
                DataStore.MainViewModel.Message = "Вы вошли";
                DataStore.NavigationService.GoBack();
                DataStore.NavigationService.Navigate(DataStore.Instance.MainBookPage);
            }

            DataStore.MainViewModel.Message = "Неверный логин или пароль";
            DataStore.MainViewModel.OpenShapka();
        }
    }
}
