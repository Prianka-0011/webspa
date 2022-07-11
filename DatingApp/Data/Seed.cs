using DatingApp.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DatingApp.Data
{
    public class Seed
    {
        public static async Task SeedUser(DataContext context)
        {
            if (await context.Users.AnyAsync()) return;
            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            foreach (var item in users)

            {
                using var hamc=new HMACSHA512();
                item.UserName = item.UserName.ToString();
                item.Password = hamc.ComputeHash(Encoding.UTF8.GetBytes("Pass$$)rd"));
                item.PasswordSalt = hamc.Key;
                context.Users.Add(item);
            } 
            await context.SaveChangesAsync();
        }
    }
}
