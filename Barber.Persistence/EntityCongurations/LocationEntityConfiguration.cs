using Barber.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barber.Persistence.EntityCongurations;

public class LocationEntityConfiguration: IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder
            .HasOne(l => l.Barber)
            .WithOne(b => b.Location)
            .HasForeignKey<Location>(l => l.BarberId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}