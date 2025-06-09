// UserService.cs
using Entity.Models;
using Infastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infastructure.Services
{
    public static class UserService
    {
        public static AppDbContext Context { get; set; }


        private static readonly byte[] AesKey = Encoding.UTF8.GetBytes("MySuperSecretKey123"); // Должно быть 16, 24 или 32 байта
        private static readonly byte[] AesIV = Encoding.UTF8.GetBytes("16ByteIV12345678"); // Всегда 16 байт

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
            user.Password = EncryptPassword(user.Password); // Шифруем пароль перед сохранением
            Context.Users.Add(user);
            await Context.SaveChangesAsync();
        }

        public static async Task UpdateUserAsync(User user)
        {
            user.Password = EncryptPassword(user.Password); // Обновляем зашифрованный пароль
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
            var user = await Context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
                return null;

            // Сравниваем зашифрованные пароли
            string encryptedInputPassword = EncryptPassword(password);
            if (user.Password == encryptedInputPassword)
                return user;

            return null;
        }

        // Шифрование пароля с помощью AES
        private static string EncryptPassword(string plainPassword)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = AesKey;
                aesAlg.IV = AesIV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainPassword);
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }

        // Расшифровка пароля (если нужно)
        private static string DecryptPassword(string encryptedPassword)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = AesKey;
                aesAlg.IV = AesIV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(encryptedPassword)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}