using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using TripPlanner.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TripPlanner.Application.Common.Interfaces;

namespace TripPlanner.Application.Trips.Queries;

[Authorize]
public record GetTripQuery(Guid id) : IRequest<TripDto?>
{
    public Guid Id { get; } = id;
}

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
    private readonly IMapper _mapper;
    private readonly string _userId;

    public GetTripQueryHandler(IApplicationDbContext context, IMapper mapper, IUser user)
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
            .Where(t => t.Participants.Any(p => p.UserId == _userId))
            .ProjectTo<TripDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken: cancellationToken);

        return trip;
    }
}