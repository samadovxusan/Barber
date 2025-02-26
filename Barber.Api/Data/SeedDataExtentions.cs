using Barber.Domain.Entities;
using Barber.Domain.Enums;
using Barber.Persistence.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace Barber.Api.Data;

public static class SeedDataExtensions
{
    public static async ValueTask InitializeSeedAsync(this IServiceProvider serviceProvider)
    {
        var appDbContext = serviceProvider.GetRequiredService<AppDbContext>();

        if (!await appDbContext.Users.AnyAsync())
            await appDbContext.SeedUsersAsync();
    }

    private static async ValueTask SeedUsersAsync(this AppDbContext dbContext)
    {
        var clients = new List<User>
        {
            new()
            {
                Id = Guid.Parse("54e16518-d140-4453-80c9-1a117dbe75fd"),
                FullName = "Husan",
                PhoneNumber = "99 843 90 13",
                Password = "husan9090AA",
                Roles = Role.Admin
            },
            new()
            {
                Id = Guid.Parse("34e16518-d140-4453-80c9-1a117dbe75fd"),
                FullName = "Bob",
                PhoneNumber = "99 123 45 67",
                Password = "bob9090AA",
                Roles = Role.Customer
            },
            new()
            {
                Id = Guid.Parse("54e16318-d140-4453-80c9-1a117dbe75fd"),
                FullName = "Funk",
                PhoneNumber = "99 765 43 21",
                Password = "funk8877AA",
                Roles = Role.Barber
            },
        };

        await dbContext.Users.AddRangeAsync(clients);
        await dbContext.SaveChangesAsync();
    }
}