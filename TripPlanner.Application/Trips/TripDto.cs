using AutoMapper;
using TripPlanner.Application.Entries;
using TripPlanner.Core.Entities;

namespace TripPlanner.Application.Trips;

public record TripDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public IReadOnlyCollection<string> Participants { get; init; } = Array.Empty<string>();
    public IReadOnlyCollection<EntryDto> Entries { get; init; } = Array.Empty<EntryDto>(); 

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Trip, TripDto>()
                .ForMember(t => t.Participants, opt => opt.MapFrom(t => t.Participants.Select(p => p.User.Name)));
        }
    }
}