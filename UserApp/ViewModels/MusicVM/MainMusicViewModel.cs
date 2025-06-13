using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Entity.Models.MusicEntity;
using Infastructure.Services.Music;

using System.Collections.ObjectModel;
using UserApp.ViewModels.Base;

namespace UserApp.ViewModels.MusicVM
{
    public partial class MainMusicViewModel : ObservableObject
    {
        public ObservableCollection<Music> Musics { get; set; }
        public ObservableCollection<Music> FilteredMusics { get; set; }
        public ObservableCollection<Actor> Actors { get; set; }

        [ObservableProperty]
        private string searchText;

        [ObservableProperty]
        bool isAdmin;

        public MainMusicViewModel()
        {
            IsAdmin = DataStore.AdminMode;
            LoadData();
        }

        private async void LoadData()
        {
            Musics = new ObservableCollection<Music>(await MusicService.GetAllMusicsAsync());
            FilteredMusics = new ObservableCollection<Music>(Musics);
            Actors = new ObservableCollection<Actor>(await ActorService.GetAllActorsAsync());

            PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(SearchText))
                {
                    FilterAndSortMusics();
                }
            };
        }

        public void FilterAndSortMusics()
        {
            if (Musics == null || !Musics.Any())
                return;

            // Фильтрация по SearchText (если не пусто)
            var filtered = string.IsNullOrWhiteSpace(SearchText)
                ? Musics.ToList() // Если строка поиска пуста, берем все треки
                : Musics.Where(music =>
                    music.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    music.Actor?.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) == true).ToList();

            // Сортировка по названию (Name)
            var sortedMusics = filtered.OrderBy(music => music.Name).ToList();

            // Обновляем FilteredMusics
            FilteredMusics.Clear();
            foreach (var music in sortedMusics)
            {
                FilteredMusics.Add(music);
            }

            // Уведомляем UI об изменении коллекции
            OnPropertyChanged(nameof(FilteredMusics));
        }

        [RelayCommand]
        void ChooseAnActor(Actor actor)
        {
            var page = DataStore.Instance.ActorPage;
            page.DataContext = actor == null ? new ActorViewModel() : new ActorViewModel(actor);
            DataStore.NavigationService.Navigate(page);
        }

        [RelayCommand]
        void AddAnActor()
        {
            var page = DataStore.Instance.ActorPage;
            page.DataContext = new ActorViewModel();
            DataStore.NavigationService.Navigate(page);
        }

        [RelayCommand]
        void ChooseAMusic(Music music)
        {
            var page = DataStore.Instance.MusicPage;
            page.DataContext = music == null ? new MusicViewModel() : new MusicViewModel(music);
            DataStore.NavigationService.Navigate(page);
        }

        [RelayCommand]
        void AddAMusic()
        {
            var page = DataStore.Instance.MusicPage;
            page.DataContext = new MusicViewModel();
            DataStore.NavigationService.Navigate(page);
        }
    }
}