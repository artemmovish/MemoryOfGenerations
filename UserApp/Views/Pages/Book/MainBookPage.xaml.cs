using CommunityToolkit.Mvvm.Messaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using UserApp.ViewModels.Base;
using UserApp.ViewModels.BookVM;

namespace UserApp.Views.Pages.Book
{
    /// <summary>
    /// Логика взаимодействия для MainBookPage.xaml
    /// </summary>
    public partial class MainBookPage : Page
    {
        public MainBookPage()
        {
            InitializeComponent(); 
            DataContext = new MainBookViewModel();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AddAuthorBtn.Visibility = DataStore.AdminMode ? Visibility.Visible : Visibility.Collapsed;
            AddBookBtn.Visibility = DataStore.AdminMode ? Visibility.Visible : Visibility.Collapsed;
            DataContext = new MainBookViewModel();
        }
    }
}
