using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Entity.Enums;
using Entity.Models;
using Infastructure.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UserApp.ViewModels.Base;

namespace UserApp.ViewModels.BookVM
{
    public partial class BookViewModel : ObservableObject
    {
        [ObservableProperty]
        Book selectedBook = new();
        [ObservableProperty]
        string photoPath;
        public ObservableCollection<Author> Authors { get; set; } = new ObservableCollection<Author>();
        [ObservableProperty]
        User currentUser;
        [ObservableProperty]
        bool isAdmin;
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

                CurrentUser = new User
                {
                    Id = 1,
                    AvatarPath = "/path/to/avatar.jpg",
                    Username = "TestUser",
                    Password = "TestPassword123",

                    // Инициализация коллекций
                    MyThoughts = new List<MyThought>
                        {
                            new MyThought
                            {
                                Id = 1,
                                BookId = 1,
                                Chapter = 1,
                                Text = "Это моя первая мысль о книге"
                            },
                            new MyThought
                            {
                                Id = 1,
                                BookId = 1,
                                Chapter = 1,
                                Text = "Это моя первая мысль о книге"
                            },
                            new MyThought
                            {
                                Id = 1,
                                BookId = 1,
                                Chapter = 1,
                                Text = "Это моя первая мысль о книге"
                            },
                            new MyThought
                            {
                                Id = 1,
                                BookId = 1,
                                Chapter = 1,
                                Text = "Это моя первая мысль о книге"
                            }
                        },

                    FavoriteBooks = new List<FavoriteBook>()
                };

                string defaultAuthorImage = "https://avatars.mds.yandex.net/get-entity_search/2362199/483039622/S168x252_2x";

                var author1 = new Author
                {
                    Id = 1,
                    Name = "Лев Толстой",
                    Biography = "Русский писатель, мыслитель, философ и публицист.",
                    BirthDate = new DateTime(1828, 9, 9),
                    DeathDate = new DateTime(1910, 11, 20),
                    PhotoPath = defaultAuthorImage,
                    InterestingFacts = "Автор романов 'Война и мир' и 'Анна Каренина'."
                };

                var author2 = new Author
                {
                    Id = 2,
                    Name = "Фёдор Достоевский",
                    Biography = "Русский писатель, мыслитель, философ и публицист.",
                    BirthDate = new DateTime(1821, 11, 11),
                    DeathDate = new DateTime(1881, 2, 9),
                    PhotoPath = defaultAuthorImage,
                    InterestingFacts = "Автор 'Преступления и наказания'."
                };

                var author3 = new Author
                {
                    Id = 3,
                    Name = "Стивен Кинг",
                    Biography = "Американский писатель, работающий в разнообразных жанрах.",
                    BirthDate = new DateTime(1947, 9, 21),
                    PhotoPath = defaultAuthorImage,
                    InterestingFacts = "Король ужасов."
                };

                Authors.Add(author1);
                Authors.Add(author2);
                Authors.Add(author3);

                return;
            }

            LoadData();
        }

        public BookViewModel(Book book)
        {
            IsAdmin = DataStore.AdminMode;
            SelectedBook = book;
            PhotoPath = book.CoverImagePath;
            LoadData();
        }

        public async Task LoadData()
        {
            Authors = new ObservableCollection<Author>(await AuthorService.GetAllAuthorsAsync());
        }

        private void LoadUser()
        {
            CurrentUser = DataStore.Instance.User;
        }

        [RelayCommand]
        public async void DeleteBook()
        {
            try
            {
                await BookService.DeleteBookAsync(SelectedBook.Id);
                DataStore.MainViewModel.Message = "Книга удалена";
            }
            catch (Exception)
            {
                MessageBox.Show("Автор не найден!");
            }
            DataStore.NavigationService.GoBack();
        }

        [RelayCommand]
        public void AddPhoto()
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Выберите изображение автора",
                Filter = "Изображения (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp|Все файлы (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;

                // Проверяем расширение файла (опционально)
                string extension = Path.GetExtension(selectedFilePath).ToLower();
                if (extension == ".png" || extension == ".jpg" || extension == ".jpeg" || extension == ".bmp")
                {
                    SelectedBook.CoverImagePath = selectedFilePath;
                    PhotoPath = selectedFilePath;
                    DataStore.MainViewModel.Message = "Фото загружено";
                }
                else
                {
                    MessageBox.Show("Выберите файл изображения (PNG, JPG, JPEG, BMP).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
        }

        [RelayCommand]
        public void AddAudio()
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Выберите аудиофайл",
                Filter = "Аудиофайлы (*.mp3;*.wav;*.ogg;*.flac)|*.mp3;*.wav;*.ogg;*.flac|Все файлы (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic),
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;

                // Проверяем расширение файла
                string extension = Path.GetExtension(selectedFilePath).ToLower();
                if (extension == ".mp3" || extension == ".wav" || extension == ".ogg" || extension == ".flac")
                {
                    SelectedBook.AudioBookPath = selectedFilePath;
                    DataStore.MainViewModel.Message = "Аудио файл загружен";
                }
                else
                {
                    MessageBox.Show("Выберите файл аудио (MP3, WAV, OGG, FLAC).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        [RelayCommand]
        public void AddAuthor(Author author)
        {
            SelectedBook.Author = author;
            SelectedBook.AuthorId = author.Id;
            DataStore.MainViewModel.Message = $"Добавлен автор: {author.Name}";
        }

        [RelayCommand]
        public async void SaveBook()
        {
            if (SelectedBook.Id == 0)
            {
                await BookService.AddBookAsync(SelectedBook);
                DataStore.MainViewModel.Message = "Книга добавлена";
                DataStore.NavigationService.GoBack();
                return;
            }
            await BookService.UpdateBookAsync(SelectedBook);
            DataStore.MainViewModel.Message = "Книга изменена";

            DataStore.NavigationService.GoBack();
        }
    }
}
