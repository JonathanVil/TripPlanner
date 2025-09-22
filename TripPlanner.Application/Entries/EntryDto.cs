using AutoMapper;
using TripPlanner.Core.Entities;

namespace TripPlanner.Application.Entries;

public record EntryDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Comment { get; init; }
    public DateTime Created { get; init; }
    public string CreatedBy { get; init; } = string.Empty;
    public Guid TripId { get; init; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Entry, EntryDto>()
                .ForMember(e => e.CreatedBy, opt => opt.MapFrom(e => e.User.Name));
        }
    }
}