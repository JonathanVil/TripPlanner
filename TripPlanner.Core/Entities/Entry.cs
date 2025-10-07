namespace TripPlanner.Core.Entities;

public class Entry : EntityBase
{
    public required User User { get; set; }
    public required Trip Trip { get; set; }
    public string UserId { get; set; } = null!;
    public Guid TripId { get; set; }
    public required string Title { get; set; }
    public string? Comment { get; set; }

    // Optional place information attached to this entry
    public string? PlaceId { get; set; }
    public string? PlaceName { get; set; }
    public string? PlaceAddress { get; set; }

    public List<Reaction> Reactions { get; set; } = [];
}