using Entity.Models;
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
        public DbSet<Book> Books { get; set; }
        public DbSet<MyThought> MyThoughts { get; set; }
        public DbSet<FavoriteBook> FavoriteBooks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; } // Новая таблица

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Правильное подключение для SQLite
                optionsBuilder.UseSqlite("Data Source=E:\\Project\\Учебный процесс\\КПиЯП\\Cursach\\MemoryOfGenerations\\Memory.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Связь Author-Book (один-ко-многим)
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Restrict); // Не удаляем автора при удалении книги

            // Остальные связи остаются без изменений
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
        }
    }
}
