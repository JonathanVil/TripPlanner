using Ardalis.GuardClauses;
using FluentValidation;
using MediatR;
using TripPlanner.Application.Common.Interfaces;
using TripPlanner.Core.Entities;

namespace TripPlanner.Application.Trips.Commands;

// [Authorize]
public class CreateTripCommand : IRequest<Guid>
{
    public string Title { get; set; } = null!;
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
    private readonly Guid _userId;

    public CreateTripCommandHandler(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _userId = Guard.Against.Null(user.Id);
    }

    public async Task<Guid> Handle(CreateTripCommand request, CancellationToken cancellationToken)
    {
        var entity = new Trip(request.Title, _userId);

        _context.Trips.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}