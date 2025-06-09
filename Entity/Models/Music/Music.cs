﻿// Music.cs
namespace Entity.Models.Music
{
    public class Music
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string MusicPath { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty; // Исправлена опечатка (было ImaagePath)
        public string TextPath { get; set; } = string.Empty;

        // Foreign key and navigation property for Actor
        public int ActorId { get; set; }
        public Actor Actor { get; set; }

        // Navigation property for PlayList (many-to-many)
        public ICollection<PlayList> PlayLists { get; set; } = new List<PlayList>();
    }
}