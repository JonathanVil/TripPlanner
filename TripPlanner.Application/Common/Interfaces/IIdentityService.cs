using TripPlanner.Application.Common.Models;

namespace TripPlanner.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(Guid userId);

    Task<(Result Result, Guid UserId)> CreateUserAsync(string userName);

    Task<Result> DeleteUserAsync(Guid userId);
}
