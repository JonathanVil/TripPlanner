namespace TripPlanner.Core.Entities;

public class Participation
{
    public Guid UserId { get; set; }
    public Guid TripId { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual Trip Trip { get; set; }
    public bool IsOwner { get; set; }
}