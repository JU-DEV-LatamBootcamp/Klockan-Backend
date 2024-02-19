using KlockanAPI.Domain.Models;

using Microsoft.EntityFrameworkCore;

namespace KlockanAPI.Infrastructure.Data.Seeders;

public static class UserSeeder
{
    public static ModelBuilder SeedUsers(this ModelBuilder modelBuilder)
    {
        Random rnd = new Random();

        modelBuilder.Entity<User>().HasData(
             new User
             {
                 Id = 1,
                 FirstName = "Martín",
                 LastName = "López",
                 Email = "martin.lopez@jala.university",
                 Avatar = $"https://randomuser.me/api/portraits/men/{rnd.Next(1, 99)}.jpg",
                 Birthdate = new DateOnly(1990, 5, 15),
                 CityId = 1,
                 RoleId = 1
             },
             new User
             {
                 Id = 2,
                 FirstName = "Lucía",
                 LastName = "Martínez",
                 Email = "lucia.martinez@jala.university",
                 Avatar = $"https://randomuser.me/api/portraits/women/{rnd.Next(1, 99)}.jpg",
                 Birthdate = new DateOnly(1988, 8, 20),
                 CityId = 3,
                 RoleId = 2
             },
             new User
             {
                 Id = 3,
                 FirstName = "Carlos",
                 LastName = "Gutiérrez",
                 Email = "carlos.gutierrez@jala.university",
                 Avatar = $"https://randomuser.me/api/portraits/men/{rnd.Next(1, 99)}.jpg",
                 Birthdate = new DateOnly(1992, 3, 10),
                 CityId = 21, // ID de una ciudad de Bolivia según el seeder de ciudades
                 RoleId = 2
             },
             new User
             {
                 Id = 4,
                 FirstName = "María",
                 LastName = "Pérez",
                 Email = "maria.perez@jala.university",
                 Avatar = $"https://randomuser.me/api/portraits/women/{rnd.Next(1, 99)}.jpg",
                 Birthdate = new DateOnly(1991, 11, 25),
                 CityId = 22, // ID de otra ciudad de Bolivia según el seeder de ciudades
                 RoleId = 3
             },
             new User
             {
                 Id = 5,
                 FirstName = "Alejandro",
                 LastName = "Rodríguez",
                 Email = "alejandro.rodriguez@jala.university",
                 Avatar = $"https://randomuser.me/api/portraits/men/{rnd.Next(1, 99)}.jpg",
                 Birthdate = new DateOnly(1985, 7, 3),
                 CityId = 35,
                 RoleId = 4
             },
             new User
             {
                 Id = 6,
                 FirstName = "Camila",
                 LastName = "Gómez",
                 Email = "camila.gomez@jala.university",
                 Avatar = $"https://randomuser.me/api/portraits/women/{rnd.Next(1, 99)}.jpg",
                 Birthdate = new DateOnly(1989, 9, 12),
                 CityId = 36,
                 RoleId = 4
             }
         );

        return modelBuilder;
    }
}
