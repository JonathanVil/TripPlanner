using Ardalis.GuardClauses;

namespace TripPlanner.Core.Entities;

public class Entry(string name, Guid userId) : EntityBase
{
    public string Name { get; set; } = Guard.Against.NullOrEmpty(name, nameof(name));
    public string? Description { get; set; }
    public Guid UserId { get; set; } = Guard.Against.Null(userId, nameof(userId));
}