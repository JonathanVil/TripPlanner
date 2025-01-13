using AutoMapper;
using TripPlanner.Application.Entries;
using TripPlanner.Core.Entities;

namespace TripPlanner.Application.Trips;

public record TripDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public IReadOnlyCollection<ParticipationDto> Participants { get; init; } = Array.Empty<ParticipationDto>();
    public IReadOnlyCollection<EntryDto> Entries { get; init; } = Array.Empty<EntryDto>();
    public string JoinCode { get; init; } = string.Empty;

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Trip, TripDto>();
        }
    }
}