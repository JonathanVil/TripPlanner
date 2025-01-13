namespace TripPlanner.Core.Entities;

public class Participation
{
    public string UserId { get; set; } = null!;
    public Guid TripId { get; set; }
    public virtual User User { get; set; } = null!;
    public virtual Trip Trip { get; set; } = null!;
    public DateTimeOffset JoinedOn { get; set; } = DateTimeOffset.UtcNow;
    public bool IsOwner { get; set; }
}