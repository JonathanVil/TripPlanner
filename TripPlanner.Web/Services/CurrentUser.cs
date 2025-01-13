using System.Text;
using TripPlanner.Application.Common.Interfaces;
using TripPlanner.Infrastructure;

namespace TripPlanner.Web.Services;

public class CurrentUser : IUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IIdentityService _identityService;

    private const string UserIdClaim = "UserId";

    public CurrentUser(IHttpContextAccessor httpContextAccessor, IIdentityService identityService)
    {
        _httpContextAccessor = httpContextAccessor;
        _identityService = identityService;
    }

    public string? Id
    {
        get
        {
            _httpContextAccessor.HttpContext!.Request.Cookies.TryGetValue(UserIdClaim, out var userId);

            if (userId == null)
                return null;

            Encoding.UTF8.GetString(Convert.FromBase64String(userId)).GetSha256Hash();
            
            // validate the user id
            var username = _identityService.GetUserName(userId);
            
            // if the user id is invalid, remove it from the cookies
            if (username == null)
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Delete(UserIdClaim);
                return null;
            }
            
            return userId;
        }
    }

    public void SetAccessKey(string key)
    {
        var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(key));
        _httpContextAccessor.HttpContext!.Response.Cookies.Append(UserIdClaim, encoded);
    }
}