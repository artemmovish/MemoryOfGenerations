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
using VersOne.Epub;

namespace UserApp.Views.Pages.Book
{
    /// <summary>
    /// Логика взаимодействия для BookPage.xaml
    /// </summary>
    public partial class BookPage : Page
    {
        public BookPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AdminPanel.Visibility = DataStore.AdminMode ? Visibility.Visible : Visibility.Collapsed;
            UserPanel.Visibility = DataStore.AdminMode ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
