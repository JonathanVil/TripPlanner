using AutoMapper;

namespace TripPlanner.Application.Trips;

public record TripSummaryDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public int ParticipantCount { get; init; }
    public int EntryCount { get; init; }
    public string Owner { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
    public string JoinCode { get; set; } = string.Empty;

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Core.Entities.Trip, TripSummaryDto>()
                .ForMember(d => d.ParticipantCount, opt => opt.MapFrom(s => s.Participants.Count))
                .ForMember(d => d.EntryCount, opt => opt.MapFrom(s => s.Entries.Count))
                .ForMember(d => d.Owner, opt => opt.MapFrom(s => s.Participants.First(p => p.IsOwner).User.Name))
                .ForMember(d => d.StartDate, opt => opt.MapFrom(s => s.Created));
        }
    }
}