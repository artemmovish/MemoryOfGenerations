using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using UserApp.ViewModels.Book;
using UserApp.Views.Pages.Book;

namespace UserApp.ViewModels.Base
{
    public class DataStore
    {
        // Приватный статический экземпляр класса
        private static DataStore _instance;

        // Приватный конструктор для предотвращения создания экземпляров извне
        private DataStore()
        {
            MainBookPage.DataContext = MainBookViewModel;
            BookPage.DataContext = BookViewModel;
        }

        // Публичное статическое свойство для доступа к экземпляру
        public static DataStore Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DataStore();
                }
                return _instance;
            }
        }

        #region Book
        public StartBookPage StartBookPage { get; set; } = new();
        public MainBookPage MainBookPage { get; set; } = new();
        public MainBookViewModel MainBookViewModel { get; set; } = new();
        public BookPage BookPage { get; set; } = new();
        public BookViewModel BookViewModel { get; set; } = new();

        #endregion
    }
}
