namespace TripPlanner.Core.Entities;

public class Entry : EntityBase
{
    public required User User { get; set; }
    public required Trip Trip { get; set; }
    public Guid UserId { get; set; }
    public Guid TripId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}