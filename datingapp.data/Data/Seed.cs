using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using datingapp.data.Entities;
using Microsoft.EntityFrameworkCore;

namespace datingapp.data.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if (await context.Users.AnyAsync()) return;

            var userData = await File.ReadAllTextAsync("UserSeedData.json");

            // in json file disregard property names small/big letters
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // transform data from json to c# object
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

            foreach (var user in users!)
            {
                using var hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt = hmac.Key;

                context.Add(user);
            }

            await context.SaveChangesAsync();
        }
    }
}