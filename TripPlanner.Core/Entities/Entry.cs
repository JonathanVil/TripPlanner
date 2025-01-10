namespace TripPlanner.Core.Entities;

public class Entry : EntityBase
{
    public required Trip Trip { get; set; }
    public Guid TripId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required ApplicationUser User { get; set; }
    public Guid UserId { get; set; }
}