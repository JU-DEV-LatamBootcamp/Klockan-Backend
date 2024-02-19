using Microsoft.EntityFrameworkCore;

using KlockanAPI.Domain.Models;

namespace KlockanAPI.Infrastructure.Data.Seeders;

public static class RoleSeeder
{
    public static ModelBuilder SeedRoles(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Admin" },
            new Role { Id = 2, Name = "Trainer" },
            new Role { Id = 3, Name = "Student" },
            new Role { Id = 4, Name = "Guest" });

        return modelBuilder;
    }
}
