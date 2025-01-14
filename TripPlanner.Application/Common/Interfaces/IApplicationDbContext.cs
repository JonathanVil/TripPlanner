using Microsoft.EntityFrameworkCore;
using TripPlanner.Core.Entities;

namespace TripPlanner.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    
    DbSet<Trip> Trips { get; }
    
    DbSet<Entry> Entries { get; }
    
    DbSet<Reaction> Reactions { get; }
    DbSet<Like> Likes { get; }
    DbSet<Dislike> Dislikes { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
