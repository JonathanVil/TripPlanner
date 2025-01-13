using System.Text;
using TripPlanner.Application.Common.Interfaces;
using TripPlanner.Infrastructure;

namespace TripPlanner.Web.Services;

public class CurrentUser : IUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private const string UserIdClaim = "UserId";

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? Id =>
        _httpContextAccessor.HttpContext!.Request.Cookies.TryGetValue(UserIdClaim, out var userId)
            ? Encoding.UTF8.GetString(Convert.FromBase64String(userId)).GetSha256Hash()
            : null;
    
    public void SetAccessKey(string key)
    {
        var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(key));
        _httpContextAccessor.HttpContext!.Response.Cookies.Append(UserIdClaim, encoded);
    }

    public void ClearAccessKey()
    {
        _httpContextAccessor.HttpContext!.Response.Cookies.Delete(UserIdClaim);
    }
}