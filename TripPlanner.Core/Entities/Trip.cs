using Ardalis.GuardClauses;

namespace TripPlanner.Core.Entities;

public class Trip(string title) : EntityBase
{
    public string Title { get; set; } = Guard.Against.NullOrEmpty(title, nameof(title));
    public List<Participation> Participants { get; set; } = new();
    public List<Entry> Entries { get; set; } = new();
}