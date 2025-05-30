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
    public class MyThoughtService
    {
        private readonly AppDbContext _context;

        public MyThoughtService(AppDbContext context)
        {
            _context = context;
        }

        // Получить все заметки
        public async Task<List<MyThought>> GetAllThoughtsAsync()
        {
            return await _context.MyThoughts
                .Include(mt => mt.User)
                .Include(mt => mt.Book)
                .ToListAsync();
        }

        // Получить заметку по ID
        public async Task<MyThought?> GetThoughtByIdAsync(int id)
        {
            return await _context.MyThoughts
                .Include(mt => mt.User)
                .Include(mt => mt.Book)
                .FirstOrDefaultAsync(mt => mt.Id == id);
        }

        // Получить заметки пользователя
        public async Task<List<MyThought>> GetUserThoughtsAsync(int userId)
        {
            return await _context.MyThoughts
                .Where(mt => mt.UserId == userId)
                .Include(mt => mt.Book)
                .ToListAsync();
        }

        // Получить заметки для книги
        public async Task<List<MyThought>> GetBookThoughtsAsync(int bookId)
        {
            return await _context.MyThoughts
                .Where(mt => mt.BookId == bookId)
                .Include(mt => mt.User)
                .ToListAsync();
        }

        // Добавить новую заметку
        public async Task AddThoughtAsync(MyThought thought)
        {
            _context.MyThoughts.Add(thought);
            await _context.SaveChangesAsync();
        }

        // Обновить заметку
        public async Task UpdateThoughtAsync(MyThought thought)
        {
            _context.MyThoughts.Update(thought);
            await _context.SaveChangesAsync();
        }

        // Удалить заметку
        public async Task DeleteThoughtAsync(int id)
        {
            var thought = await _context.MyThoughts.FindAsync(id);
            if (thought != null)
            {
                _context.MyThoughts.Remove(thought);
                await _context.SaveChangesAsync();
            }
        }
    }
}
