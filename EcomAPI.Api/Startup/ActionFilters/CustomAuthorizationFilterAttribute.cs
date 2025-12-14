using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

namespace EcomAPI.Api.Startup.ActionFilters
{
    //public class CustomAuthorizationFilterAttribute(UserRoleLookup[]? _minAccessLevelRole) : ActionFilterAttribute
    //{
    //    public override void OnActionExecuted(ActionExecutedContext filterContext)
    //    {
    //    }
    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        if (filterContext.Filters.Any(x => x.GetType() == typeof(AllowAnonymousFilter)))
    //            return;

    //        var user = GetUser(filterContext.HttpContext);

    //        AuthorizeByUserRole(_minAccessLevelRole, user);
    //    }
    //    private static void AuthorizeByUserRole(UserRoleLookup[]? _minAccessLevelRole, LoggedInUser user)
    //    {
    //        if (_minAccessLevelRole?.Length == 0)
    //        {
    //            return;
    //        }

    //        if (_minAccessLevelRole?.Length == 1 && user.RoleId > _minAccessLevelRole[0])
    //        {
    //            throw new ForbiddenAccessException("This module is not available to the user.");
    //        }

    //        if (_minAccessLevelRole?.Length > 1 && user.RoleId > _minAccessLevelRole.Min())
    //        {
    //            throw new ForbiddenAccessException("This module is not available to the user.");
    //        }
    //    }


    //    internal LoggedInUser GetUser(HttpContext filterContext)
    //    {
    //        return JsonConvert.DeserializeObject<LoggedInUser>(filterContext.User.FindFirstValue(ClaimTypes.UserData)!)!;
    //    }
    //}
}
