using Entity.Models;
using Entity.Models.Music;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infastructure.Context
{
    public class AppDbContext : DbContext
    {
        // Существующие DbSet
        public DbSet<Book> Books { get; set; }
        public DbSet<MyThought> MyThoughts { get; set; }
        public DbSet<FavoriteBook> FavoriteBooks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }

        // Новые DbSet для музыки
        public DbSet<Music> Musics { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<PlayList> PlayLists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=E:\\Project\\Учебный процесс\\КПиЯП\\Cursach\\MemoryOfGenerations\\Memory.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Существующие связи
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MyThought>()
                .HasOne(mt => mt.User)
                .WithMany(u => u.MyThoughts)
                .HasForeignKey(mt => mt.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MyThought>()
                .HasOne(mt => mt.Book)
                .WithMany(b => b.MyThoughts)
                .HasForeignKey(mt => mt.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FavoriteBook>()
                .HasOne(fb => fb.User)
                .WithMany(u => u.FavoriteBooks)
                .HasForeignKey(fb => fb.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FavoriteBook>()
                .HasOne(fb => fb.Book)
                .WithMany(b => b.FavoriteBooks)
                .HasForeignKey(fb => fb.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FavoriteBook>()
                .HasIndex(fb => new { fb.UserId, fb.BookId })
                .IsUnique();

            // Новые связи для музыки
            // Связь Actor-Music (один-ко-многим)
            modelBuilder.Entity<Music>()
                .HasOne(m => m.Actor)
                .WithMany(a => a.Musics)
                .HasForeignKey(m => m.ActorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Связь Music-PlayList (многие-ко-многим)
            modelBuilder.Entity<Music>()
                .HasMany(m => m.PlayLists)
                .WithMany(p => p.Musics)
                .UsingEntity<Dictionary<string, object>>(
                    "MusicPlayList",
                    j => j.HasOne<PlayList>().WithMany().HasForeignKey("PlayListId"),
                    j => j.HasOne<Music>().WithMany().HasForeignKey("MusicId"),
                    j => j.ToTable("MusicPlayList"));

            // Связь User-Music (многие-ко-многим для избранных треков)
            modelBuilder.Entity<User>()
                .HasMany(u => u.FavoriteMusics)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "UserFavoriteMusic",
                    j => j.HasOne<Music>().WithMany().HasForeignKey("MusicId"),
                    j => j.HasOne<User>().WithMany().HasForeignKey("UserId"),
                    j => j.ToTable("UserFavoriteMusic"));
        }
    }
}