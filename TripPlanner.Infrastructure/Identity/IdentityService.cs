using TripPlanner.Application.Common.Interfaces;
using TripPlanner.Application.Common.Models;
using TripPlanner.Core.Entities;
using TripPlanner.Infrastructure.Data;

namespace TripPlanner.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly ApplicationDbContext _context;

    public IdentityService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string?> GetUserNameAsync(Guid userId)
    {
        var user = await _context.Users.FindAsync(userId);
        return user?.Name;
    }

    public async Task<(Result Result, Guid UserId)> CreateUserAsync(string name)
    {
        var user = new User { Name = name };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return (Result.Success(), user.Id);
    }

    public async Task<Result> DeleteUserAsync(Guid userId)
    {
        var user = await _context.Users.FindAsync(userId);

        if (user == null)
        {
            return Result.Failure(["User not found."]);
        }

        await DeleteUserAsync(user);

        return Result.Success();
    }

    public async Task<Result> DeleteUserAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return Result.Success();
    }
}