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

        MainFrame.Navigate(DataStore.Instance.MainBookPage);
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
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