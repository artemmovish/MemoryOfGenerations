using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Entity.Models;
using Infastructure.Services;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.ComponentModel; // Не забудьте using!
using System.Windows;
using UserApp.ViewModels.Base;
using Entity.Enums;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Navigation; // DependencyObject

namespace UserApp.ViewModels.BookVM
{
    

    public partial class MainBookViewModel : ObservableObject
    {
        public ObservableCollection<Book> Books { get; set; }

        public ObservableCollection<Book> SortBooks { get; set; }

        public ObservableCollection<Author> Authors { get; set; }

        [ObservableProperty]
        private string searchText;
        [ObservableProperty]
        bool isAdmin;
        public MainBookViewModel()
        {
            IsAdmin = DataStore.AdminMode;
            if (DataStore.IsInDesignMode)
            {
                LoadTestData();
                return;
            }
            LoadData();
        }

        private async void LoadData()
        {
            Books = new ObservableCollection<Book>(await BookService.GetAllBooksAsync());
            SortBooks = new ObservableCollection<Book>(Books);

            Authors = new ObservableCollection<Author>(await AuthorService.GetAllAuthorsAsync());

            PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(SearchText))
                {
                    FilterAndSortBooks();
                }
            };
        }

        public void FilterAndSortBooks()
        {
            if (Books == null || !Books.Any())
                return;

            // Фильтрация по SearchText (если не пусто)
            var filtered = string.IsNullOrWhiteSpace(SearchText)
                ? Books.ToList() // Если строка поиска пуста, берем все книги
                : Books.Where(book =>
                    book.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList();

            // Сортировка по названию (Title)
            var sortedBooks = filtered.OrderBy(book => book.Title).ToList();

            // Обновляем FilteredBooks
            SortBooks.Clear();
            foreach (var book in sortedBooks)
            {
                SortBooks.Add(book);
            }

            // Уведомляем UI об изменении коллекции
            OnPropertyChanged(nameof(SortBooks));
        }

        private void LoadTestData()
        {
            Authors = new ObservableCollection<Author>();
            Books = new ObservableCollection<Book>();
            // Common image paths
            string defaultAuthorImage = "https://avatars.mds.yandex.net/get-entity_search/10920629/1132268124/S600xU_2x";
            string defaultBookImage = "https://avatars.mds.yandex.net/get-entity_search/2362199/483039622/S168x252_2x";

            // Create test authors
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

            // Create test books
            var book1 = new Book
            {
                Id = 1,
                Title = "Война и мир",
                AuthorId = 1,
                Author = author1,
                Description = "Роман-эпопея, описывающий русское общество в эпоху войн против Наполеона.",
                Genre = Genre.Роман,
                CoverImagePath = defaultBookImage,
                BookFilePath = @"E:\Project\Учебный процесс\КПиЯП\Cursach\DataMemory\Маленький принц\Маленький принц.epub",
                AudioBookPath = @"E:\Project\Учебный процесс\КПиЯП\Cursach\DataMemory\Маленький принц\Маленький принц.mp3"
            };

            var book2 = new Book
            {
                Id = 2,
                Title = "Анна Каренина",
                AuthorId = 1,
                Author = author1,
                Description = "Трагическая история любви замужней женщины.",
                Genre = Genre.Роман,
                CoverImagePath = defaultBookImage,
                BookFilePath = "anna_karenina.pdf"
            };

            var book3 = new Book
            {
                Id = 3,
                Title = "Преступление и наказание",
                AuthorId = 2,
                Author = author2,
                Description = "Психологический роман о преступлении и его последствиях.",
                Genre = Genre.Психология,
                CoverImagePath = defaultBookImage,
                BookFilePath = "crime_and_punishment.pdf"
            };

            var book4 = new Book
            {
                Id = 4,
                Title = "Оно",
                AuthorId = 3,
                Author = author3,
                Description = "Роман ужасов о древнем зле, терроризирующем маленький городок.",
                Genre = Genre.Ужасы,
                CoverImagePath = defaultBookImage,
                BookFilePath = "it.pdf"
            };

            Books.Add(book1);
            Books.Add(book2);
            Books.Add(book3);
            Books.Add(book4);

            // Add books to authors' collections
            author1.Books.Add(book1);
            author1.Books.Add(book2);
            author2.Books.Add(book3);
            author3.Books.Add(book4);
        }

        [RelayCommand]
        void ChooseAnAuthor(Author author)
        {
            var page = DataStore.Instance.AuthorPage;

            page.DataContext = author == null ? new AuthorViewModel() : new AuthorViewModel(author);

            DataStore.NavigationService.Navigate(page);
        }

        [RelayCommand]
        void AddAnAuthor()
        {
            var page = DataStore.Instance.AuthorPage;

            page.DataContext = new AuthorViewModel();

            DataStore.NavigationService.Navigate(page);
        }

        [RelayCommand]
        void ChooseAnBook(Book book)
        {
            var page = DataStore.Instance.BookPage;

            page.DataContext = book == null ? new BookViewModel() : new BookViewModel(book);

            DataStore.NavigationService.Navigate(page);
        }
        [RelayCommand]
        void AddAnBook()
        {
            var page = DataStore.Instance.BookPage;

            page.DataContext = new BookViewModel();

            DataStore.NavigationService.Navigate(page);
        }
    }
}
