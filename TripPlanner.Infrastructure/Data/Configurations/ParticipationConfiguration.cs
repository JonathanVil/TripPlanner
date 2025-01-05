using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripPlanner.Core.Entities;

namespace TripPlanner.Infrastructure.Data.Configurations;

public class ParticipationConfiguration : IEntityTypeConfiguration<Participation>
{
    public void Configure(EntityTypeBuilder<Participation> builder)
    {
        builder.HasKey(p => new { p.UserId, p.TripId });

        // builder.HasOne<ApplicationUser>(p => p.User)
        //     .WithMany(u => u.Participations)
        //     .HasForeignKey(p => p.UserId);
        //
        // builder.HasOne<Trip>(p => p.Trip)
        //     .WithMany(t => t.Participants)
        //     .HasForeignKey(p => p.TripId);
    }
}