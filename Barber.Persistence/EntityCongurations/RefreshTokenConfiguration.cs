﻿using Barber.Application.Auth.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barber.Persistence.EntityCongurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Token).HasMaxLength(200);

        builder.HasIndex(r => r.Token).IsUnique();
        
        builder.HasOne(r => r.User).WithMany().HasForeignKey(r => r.UserId);
    }
}