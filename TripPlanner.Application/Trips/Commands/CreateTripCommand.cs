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
    public DateTimeOffset? StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }
}

public class CreateTripCommandValidator : AbstractValidator<CreateTripCommand>
{
    public CreateTripCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .WithMessage("Title must not exceed 200 characters")
            .NotEmpty()
            .WithMessage("Title is required");

        RuleFor(v => v.StartDate)
            .NotEmpty()
            .WithMessage("Start date is required");

        RuleFor(v => v.EndDate)
            .NotEmpty()
            .WithMessage("End date is required")
            .GreaterThan(v => v.StartDate)
            .WithMessage("End date must be after start date");
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
        
        Guard.Against.Null(request.StartDate, nameof(request.StartDate));
        Guard.Against.Null(request.EndDate, nameof(request.EndDate));

        var entity = new Trip(request.Title, request.StartDate.Value, request.EndDate.Value);
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