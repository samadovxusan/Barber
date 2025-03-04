using Barber.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barber.Persistence.EntityCongurations;

public class ImagesConfiguration : IEntityTypeConfiguration<Images>
{
    public void Configure(EntityTypeBuilder<Images> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.ImagePath).HasMaxLength(255);

        builder.HasOne(i => i.Barber)
            .WithMany(b => b.Images)
            .HasForeignKey(i => i.BarberId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}