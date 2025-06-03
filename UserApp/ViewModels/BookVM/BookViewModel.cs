using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Entity.Enums;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.ViewModels.Base;

namespace UserApp.ViewModels.BookVM
{
    public partial class BookViewModel : ObservableObject
    {
        [ObservableProperty]
        Book selectedBook;

        public BookViewModel()
        {
            if (DataStore.IsInDesignMode)
            {
                Author author = new()
                {
                    Name = "Антуан де Сент-Экзюпери"
                };

                SelectedBook = new Book
                {
                    Id = 1,
                    Title = "Маленький принц",
                    AuthorId = 1,
                    Author = author,
                    Description = "Маленький принц – владелец собственной планеты, ставшей домом. Персонаж полюбил Розу. Но цветок оказался капризным. В итоге мальчик оставил подопечную и от скуки отправился в космическое путешествие. Маленький принц посетил 7 планет и познакомился с их обитателями. Основные герои сюжета изменили мировоззрение юного странника.",
                    Genre = Genre.Фэнтези,
                    CoverImagePath = @"E:\Project\Учебный процесс\КПиЯП\Cursach\DataMemory\Маленький принц\Маленький принц.jfif",
                    BookFilePath = @"E:\Project\Учебный процесс\КПиЯП\Cursach\DataMemory\Маленький принц\Маленький принц.epub",
                    AudioBookPath = @"E:\Project\Учебный процесс\КПиЯП\Cursach\DataMemory\Маленький принц\Маленький принц.mp3"
                };
                return;
            }
        }
    }
}
