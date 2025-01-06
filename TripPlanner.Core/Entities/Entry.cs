using Ardalis.GuardClauses;

namespace TripPlanner.Core.Entities;

public class Entry(Trip trip, string name, ApplicationUser user) : EntityBase
{
    public virtual Trip Trip { get; set; } = Guard.Against.Null(trip, nameof(trip));
    public Guid TripId { get; set; }
    public string Name { get; set; } = Guard.Against.NullOrEmpty(name, nameof(name));
    public string? Description { get; set; }
    public virtual ApplicationUser User { get; set; } = Guard.Against.Null(user, nameof(user));
    public Guid UserId { get; set; }
}