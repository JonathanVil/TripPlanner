using TripPlanner.Application.Common.Interfaces;
using TripPlanner.Core.Entities;

namespace TripPlanner.Application.Trips;

public record ParticipationDto
{
    public string UserName { get; init; } = string.Empty;
    public DateTime JoinedOn { get; init; }
    public bool IsOwner { get; init; }
}

public class ParticipationMapper : IMapper<Participation, ParticipationDto>
{
    public ParticipationDto Map(Participation from)
    {
        return new ParticipationDto
        {
            UserName = from.User.Name,
            JoinedOn = from.JoinedOn,
            IsOwner = from.IsOwner
        };
    }
}