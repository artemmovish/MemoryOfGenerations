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
using UserApp.ViewModels.BookVM;

namespace UserApp.Views.Pages.Book
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage(string hexColor)
        {
            InitializeComponent();
            SetBackgroundColor(hexColor);
        }

        public ProfilePage()
        {
            InitializeComponent();
        }

        private void SetBackgroundColor(string hexColor)
        {
            try
            {
                // Конвертируем HEX в SolidColorBrush
                var color = (Color)ColorConverter.ConvertFromString(hexColor);
                panel.Background = new SolidColorBrush(color);

                // Или для конкретного элемента, например, MainGrid:
                // MainGrid.Background = new SolidColorBrush(color);
            }
            catch
            {
                // Обработка ошибки (например, если цвет в неверном формате)
                panel.Background = Brushes.White; // Цвет по умолчанию
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var page = new UpdateProfilePage(panel.Background.ToString());

            page.DataContext = this.DataContext;

            DataStore.NavigationService.Navigate(page);
        }
    }
}
