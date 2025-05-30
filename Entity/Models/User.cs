using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
