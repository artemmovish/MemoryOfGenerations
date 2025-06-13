// PlayList.cs
namespace Entity.Models.MusicEntity
{
    public class PlayList
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Foreign key for User
        public int UserId { get; set; }
        public User User { get; set; }

        // Navigation property for Music (many-to-many)
        public ICollection<Music> Musics { get; set; } = new List<Music>();
    }
}