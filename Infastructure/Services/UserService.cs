// UserService.cs
using Entity.Models;
using Infastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infastructure.Services
{
    public static class UserService
    {
        public static AppDbContext Context { get; set; }

        public static async Task<List<User>> GetAllUsersAsync()
        {
            return await Context.Users.ToListAsync();
        }

        public static async Task<User?> GetUserByIdAsync(int id)
        {
            return await Context.Users.FindAsync(id);
        }

        public static async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await Context.Users
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public static async Task AddUserAsync(User user)
        {
            Context.Users.Add(user);
            await Context.SaveChangesAsync();
        }

        public static async Task UpdateUserAsync(User user)
        {
            Context.Users.Update(user);
            await Context.SaveChangesAsync();
        }

        public static async Task DeleteUserAsync(int id)
        {
            var user = await Context.Users.FindAsync(id);
            if (user != null)
            {
                Context.Users.Remove(user);
                await Context.SaveChangesAsync();
            }
        }

        public static async Task<User?> AuthenticateAsync(string username, string password)
        {
            return await Context.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }
    }
}