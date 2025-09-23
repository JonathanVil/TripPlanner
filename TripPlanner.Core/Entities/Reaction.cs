using TripPlanner.Core.Enums;

namespace TripPlanner.Core.Entities;

public class Reaction : EntityBase
{
    public ReactionType Type { get; init; }
    public virtual User User { get; set; } = null!;
    public virtual Entry Entry { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public Guid EntryId { get; set; }
}