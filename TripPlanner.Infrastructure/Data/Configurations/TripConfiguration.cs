using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripPlanner.Core.Entities;

namespace TripPlanner.Infrastructure.Data.Configurations;

public class TripConfiguration : IEntityTypeConfiguration<Trip>
{
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
        builder.Property(t => t.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(t => t.JoinCode)
            .IsUnique();
        
        builder.Property(t => t.JoinCode)
            .HasMaxLength(6)
            .IsFixedLength();
    }
}