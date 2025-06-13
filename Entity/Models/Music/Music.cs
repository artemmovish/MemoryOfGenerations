// Music.cs
using Entity.Models.Music;

namespace Entity.Models.MusicEntity
{
    public class Music
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string MusicPath { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public string TextPath { get; set; } = string.Empty;

        public ICollection<MusicActor> MusicActors { get; set; } = new List<MusicActor>();
        public ICollection<PlayList> PlayLists { get; set; } = new List<PlayList>();
        public ICollection<FavoriteMusic> FavoriteMusics { get; set; } = new List<FavoriteMusic>();
    }
}