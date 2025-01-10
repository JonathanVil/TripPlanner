using Galerie.Application.Common.Models;
using TripPlanner.Application.Common.Interfaces;
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
        throw new NotImplementedException();
    }

    public async Task<(Result Result, Guid UserId)> CreateUserAsync(string userName, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsInRoleAsync(Guid userId, string role)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AuthorizeAsync(Guid userId, string policyName)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> DeleteUserAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> DeleteUserAsync(User user)
    {
        throw new NotImplementedException();
    }
}