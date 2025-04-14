using Barber.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barber.Persistence.EntityCongurations;

public class BarberEntityConfiguration : IEntityTypeConfiguration<Domain.Entities.Barber>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Barber> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.FullName).IsRequired().HasMaxLength(100);

        // Relationship: Barber -> Booking (One-to-Many)
        builder.HasMany(b => b.Bookings)
            .WithOne(b => b.Barber)
            .HasForeignKey(b => b.BarberId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(b => b.BarberWorkingTime)
            .WithOne(ds => ds.Barber)
            .HasForeignKey(ds => ds.BarberId)
            .OnDelete(DeleteBehavior.Cascade);
        
  
    }
}