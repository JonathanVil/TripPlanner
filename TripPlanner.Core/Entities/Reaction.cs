using TripPlanner.Core.Enums;

namespace TripPlanner.Core.Entities;

public abstract class Reaction : EntityBase
{
    public abstract ReactionType Type { get; }
    public virtual User User { get; set; } = null!;
    public virtual Entry Entry { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public Guid EntryId { get; set; }
}

public class Like : Reaction
{
    public override ReactionType Type => ReactionType.Like;
}

public class Dislike : Reaction
{
    public override ReactionType Type => ReactionType.Dislike;
}