using Barber.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Barber.Persistence.DataContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
    public DbSet<User> Users { get; set; }
}