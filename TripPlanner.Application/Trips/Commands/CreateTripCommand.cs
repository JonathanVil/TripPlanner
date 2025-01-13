using Ardalis.GuardClauses;
using FluentValidation;
using MediatR;
using TripPlanner.Application.Common.Interfaces;
using TripPlanner.Core.Entities;

namespace TripPlanner.Application.Trips.Commands;

// [Authorize]
public record CreateTripCommand : IRequest<Guid>
{
    public string Title { get; set; } = null!;
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
}

public class CreateTripCommandValidator : AbstractValidator<CreateTripCommand>
{
    public CreateTripCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotEmpty();
    }
}

public class CreateTripCommandHandler : IRequestHandler<CreateTripCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly string _userId;

    public CreateTripCommandHandler(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _userId = Guard.Against.Null(user.Id);
    }

    public async Task<Guid> Handle(CreateTripCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(_userId, cancellationToken);

        Guard.Against.Null(user, nameof(user));

        var entity = new Trip(request.Title);
        entity.Participants.Add(new Participation
        {
            UserId = _userId,
            TripId = entity.Id,
            IsOwner = true
        });

        _context.Trips.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}