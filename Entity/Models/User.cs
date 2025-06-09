using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Models.Music;
namespace Entity.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? AvatarPath { get; set; }
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";

        // Navigation properties
        public ICollection<MyThought> MyThoughts { get; set; }
        public ICollection<FavoriteBook> FavoriteBooks { get; set; }

        // Navigation properties for music
        public ICollection<Music.Music> FavoriteMusics { get; set; } = new List<Music.Music>();
        
    }
}
