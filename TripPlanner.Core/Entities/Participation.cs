namespace TripPlanner.Core.Entities;

public class Participation
{
    public string UserId { get; set; }
    public Guid TripId { get; set; }
    public virtual User User { get; set; }
    public virtual Trip Trip { get; set; }
    public DateTimeOffset JoinedOn { get; set; } = DateTimeOffset.UtcNow;
    public bool IsOwner { get; set; }
}