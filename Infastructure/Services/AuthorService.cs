using Entity.Models;
using Infastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infastructure.Services
{
    public class AuthorService
    {
        private readonly AppDbContext _context;

        public AuthorService(AppDbContext context)
        {
            _context = context;
        }

        // Получить всех авторов
        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            return await _context.Authors
                .Include(a => a.Books)
                .ToListAsync();
        }

        // Получить автора по ID
        public async Task<Author?> GetAuthorByIdAsync(int id)
        {
            return await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        // Поиск авторов по имени
        public async Task<List<Author>> SearchAuthorsAsync(string name)
        {
            return await _context.Authors
                .Where(a => a.Name.Contains(name))
                .ToListAsync();
        }

        // Добавить нового автора
        public async Task AddAuthorAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
        }

        // Обновить автора
        public async Task UpdateAuthorAsync(Author author)
        {
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
        }

        // Удалить автора и все его книги
        public async Task DeleteAuthorWithBooksAsync(int id)
        {
            var author = await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (author == null)
                return;

            // Удаляем все книги автора
            _context.Books.RemoveRange(author.Books);

            // Удаляем самого автора
            _context.Authors.Remove(author);

            await _context.SaveChangesAsync();
        }

        // Получить книги автора
        public async Task<List<Book>> GetAuthorBooksAsync(int authorId)
        {
            return await _context.Books
                .Where(b => b.AuthorId == authorId)
                .ToListAsync();
        }
    }
}
