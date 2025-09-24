using Ardalis.GuardClauses;
using FluentValidation;
using TripPlanner.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TripPlanner.Application.Common.Interfaces;
using TripPlanner.Core.Entities;

namespace TripPlanner.Application.Trips.Queries;

[Authorize]
public record GetTripQuery(Guid Id) : IRequest<TripDto?>;

public class GetTripQueryValidator : AbstractValidator<GetTripQuery>
{
    public GetTripQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

public class GetTripQueryHandler : IRequestHandler<GetTripQuery, TripDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper<Trip, TripDto> _mapper;
    private readonly string _userId;

    public GetTripQueryHandler(IApplicationDbContext context, IMapper<Trip, TripDto> mapper, IUser user)
    {
        _context = context;
        _mapper = mapper;
        _userId = Guard.Against.Null(user.Id);
    }

    public async Task<TripDto?> Handle(GetTripQuery request, CancellationToken cancellationToken)
    {
        var trip = await _context.Trips
            .AsNoTracking()
            .Include(t => t.Participants)
            .Include(t => t.Entries)
            .ThenInclude(e => e.Reactions)
            .Where(t => t.Participants.Any(p => p.UserId == _userId))
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken: cancellationToken);

        if (trip == null) return null;
        
        return _mapper.Map(trip);
    }
}