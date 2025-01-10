using TripPlanner.Application.Common.Interfaces;

namespace TripPlanner.Web.Services;

public class CurrentUser : IUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private const string UserIdClaim = "UserId";

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public Guid? Id => _httpContextAccessor.HttpContext!.Request.Cookies.TryGetValue(UserIdClaim, out var userId) ? Guid.Parse(userId) : null;
    
    public async Task SetUserIdAsync(Guid userId) => _httpContextAccessor.HttpContext!.Response.Cookies.Append(UserIdClaim, userId.ToString());
}
