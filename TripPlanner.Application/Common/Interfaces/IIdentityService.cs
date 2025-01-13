using TripPlanner.Application.Common.Models;

namespace TripPlanner.Application.Common.Interfaces;

public interface IIdentityService
{
    public string? GetUserName(string userId);
    Task<string?> GetUserNameAsync(string userId);
    
    Task<(Result Result, string AccessKey)> CreateUserAsync(string userName);

    Task<Result> DeleteUserAsync(Guid userId);
}
