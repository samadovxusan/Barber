using Barber.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barber.Persistence.EntityCongurations;

public class BookingEntityConfiguration:IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.AppointmentTime).IsRequired();
        builder.Property(b => b.Status).IsRequired().HasMaxLength(50);

        // Relationships
        builder.HasOne(b => b.User)
            .WithMany(u => u.Bookings)
            .HasForeignKey(b => b.UserId);

        builder.HasOne(b => b.Barber)
            .WithMany(b => b.Bookings)
            .HasForeignKey(b => b.BarberId);
    }
}