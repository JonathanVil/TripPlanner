using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TripPlanner.Application.Common.Interfaces;

namespace TripPlanner.Application.Trips.Queries;

public record GetTripSummaryQuery(string JoinCode) : IRequest<TripSummaryDto?>;

public class GetTripSummaryQueryValidator : AbstractValidator<GetTripSummaryQuery>
{
    public GetTripSummaryQueryValidator()
    {
        RuleFor(x => x.JoinCode).NotEmpty();
    }
}

public class GetTripSummaryQueryHandler : IRequestHandler<GetTripSummaryQuery, TripSummaryDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTripSummaryQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TripSummaryDto?> Handle(GetTripSummaryQuery request, CancellationToken cancellationToken)
    {
        var trip = await _context.Trips
            .AsNoTracking()
            .Include(t => t.Participants)
            .ProjectTo<TripSummaryDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(t => t.JoinCode == request.JoinCode, cancellationToken: cancellationToken);

        return trip;
    }
}