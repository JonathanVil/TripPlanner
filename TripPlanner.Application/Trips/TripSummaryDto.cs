using TripPlanner.Application.Common.Interfaces;
using TripPlanner.Core.Entities;

namespace TripPlanner.Application.Trips;

public record TripSummaryDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public int ParticipantCount { get; init; }
    public int EntryCount { get; init; }
    public string Owner { get; init; } = string.Empty;
    public string JoinCode { get; set; } = string.Empty;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
}

public class TripSummaryMapper(IMapper<Participation, ParticipationDto> participationMapper) : IMapper<Trip, TripSummaryDto>
{
    public TripSummaryDto Map(Trip from)
    {
        return new TripSummaryDto
        {
            Id = from.Id,
            Title = from.Title,
            ParticipantCount = from.Participants.Count,
            EntryCount = from.Entries.Count,
            Owner = from.Participants.First(p => p.IsOwner).User.Name,
            JoinCode = from.JoinCode,
            StartDate = from.StartDate,
            EndDate = from.EndDate
        };
    }
}