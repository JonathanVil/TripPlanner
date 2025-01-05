namespace TripPlanner.Core.Entities;

public class Entry
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required User User { get; set; }
}