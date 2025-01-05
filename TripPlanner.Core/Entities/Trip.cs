using Ardalis.GuardClauses;

namespace TripPlanner.Core.Entities;

public class Trip(string title, Guid userId) : EntityBase
{
    public string Title { get; set; } = Guard.Against.NullOrEmpty(title, nameof(title));
    public Guid UserId { get; set; } = Guard.Against.Null(userId, nameof(userId));
}