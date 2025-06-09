using Infastructure.Context;
using Infastructure.Services;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserApp.ViewModels;
using UserApp.ViewModels.Base;
using UserApp.Views.Pages.Book;

namespace UserApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        var context = new AppDbContext();

        // Инициализация сервисов
        AuthorService.Context = context;
        BookService.Context = context;
        FavoriteBookService.Context = context;
        MyThoughtService.Context = context;
        UserService.Context = context;

        MainFrame.Navigate(DataStore.Instance.StartBookPage);
        DataStore.NavigationService = MainFrame.NavigationService;

        DataContext = DataStore.MainViewModel;

        DataStore.MainViewModel.OpenShapka = OpenShapka;
        DataStore.MainViewModel.CloseShapka = CloseShapka;
    }

    private void Icon_Click(object sender, RoutedEventArgs e)
    {
        MainFrame.NavigationService.GoBack();
    }

    public void OpenShapka()
    {
        Shapka.Visibility = Visibility.Visible;
    }

    public void CloseShapka()
    {
        Shapka.Visibility = Visibility.Collapsed;
    }
    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.F1)
        {
            // Переключаем видимость шапки при нажатии F1
            Shapka.Visibility = Shapka.Visibility == Visibility.Visible
                ? Visibility.Collapsed
                : Visibility.Visible;
        }

        if (e.Key == Key.Escape)
        {
            var result = MessageBox.Show(
                "Закрыть приложение?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown(); // Закрыть приложение
            }
        }
    }
}