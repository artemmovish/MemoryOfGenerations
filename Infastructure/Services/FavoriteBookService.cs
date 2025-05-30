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
    public class FavoriteBookService
    {
        private readonly AppDbContext _context;

        public FavoriteBookService(AppDbContext context)
        {
            _context = context;
        }

        // Получить все избранные книги пользователя
        public async Task<List<FavoriteBook>> GetUserFavoritesAsync(int userId)
        {
            return await _context.FavoriteBooks
                .Where(fb => fb.UserId == userId)
                .Include(fb => fb.Book)
                .ToListAsync();
        }

        // Проверить, есть ли книга в избранном
        public async Task<bool> IsBookFavoriteAsync(int userId, int bookId)
        {
            return await _context.FavoriteBooks
                .AnyAsync(fb => fb.UserId == userId && fb.BookId == bookId);
        }

        // Добавить книгу в избранное
        public async Task AddFavoriteAsync(FavoriteBook favorite)
        {
            if (!await IsBookFavoriteAsync(favorite.UserId, favorite.BookId))
            {
                _context.FavoriteBooks.Add(favorite);
                await _context.SaveChangesAsync();
            }
        }

        // Удалить книгу из избранного
        public async Task RemoveFavoriteAsync(int userId, int bookId)
        {
            var favorite = await _context.FavoriteBooks
                .FirstOrDefaultAsync(fb => fb.UserId == userId && fb.BookId == bookId);

            if (favorite != null)
            {
                _context.FavoriteBooks.Remove(favorite);
                await _context.SaveChangesAsync();
            }
        }

        // Получить количество избранных книг пользователя
        public async Task<int> GetFavoriteCountAsync(int userId)
        {
            return await _context.FavoriteBooks
                .CountAsync(fb => fb.UserId == userId);
        }
    }
}
