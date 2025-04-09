using System.Net.Mime;
using Barber.Application.Auth.Models;
using Barber.Domain.Entities;
using Barber.Domain.Enums;
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
    public DbSet<BarbarBusyTime> BarbarBusyTimes { get; set; }
    public DbSet<BarberDailySchedule?> BarberDailySchedules { get; set; }
    public DbSet<Domain.Entities.Barber> Barbers { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Images> Imageses { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

}