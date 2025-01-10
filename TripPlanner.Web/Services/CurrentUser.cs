using System.Security.Claims;
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
    
    public Guid? Id => Guid.TryParse(_httpContextAccessor.HttpContext?.Request.Cookies[UserIdClaim], out var result)
        ? result
        : null;
}
