using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Core.Entities;

namespace TecPurisima.School.Api.Seeders;

public class AdminSeeder
{
    public static async Task SeedSuperAdminAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var adminRepo = scope.ServiceProvider.GetRequiredService<IAdminRepository>();

        var existing = await adminRepo.GetByUsernameAsync("superadmin");
        if (existing == null)
        {
            var superAdmin = new Admin
            {
                FullName = "Super Admin",
                User = "superadmin",
                Password = "12345", // idealmente un hash
                Email = "superadmin@escuela.com",
                Role = "superadmin"
            };

            await adminRepo.CreateAsync(superAdmin);
        }
    }

    /*private static string HashPassword(string password)
    {
        // Aquí deberías usar una librería real como BCrypt.Net
        return BCrypt.Net.BCrypt.HashPassword(password);
    }*/
}//Password = HashPassword("supersecurepassword"), // usa hash real