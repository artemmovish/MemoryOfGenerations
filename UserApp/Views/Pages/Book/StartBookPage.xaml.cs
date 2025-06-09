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

namespace UserApp.Views.Pages.Book
{
    /// <summary>
    /// Логика взаимодействия для StartBookPage.xaml
    /// </summary>
    public partial class StartBookPage : Page
    {
        public StartBookPage()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(DataStore.Instance.MainBookPage);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(DataStore.Instance.MainBookPage);
        }
    }
}
