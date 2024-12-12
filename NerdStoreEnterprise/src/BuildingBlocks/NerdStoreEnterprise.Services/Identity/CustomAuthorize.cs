using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace NerdStoreEnterprise.Services.Identity;

public class CustomAuthorize
{
    public static bool ValidateUserClaims(HttpContext httpContext, string claimName, string claimValue)
    {
        return httpContext.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
    }
}

public class ClaimsAuthorizeAttribute : TypeFilterAttribute
{
    public ClaimsAuthorizeAttribute(string claimName, string claimValue)
        : base(typeof(ClaimRequirementFilter))
    {
        Arguments = [new Claim(claimName, claimValue)];
    }
}

public class ClaimRequirementFilter(Claim claim) : IAuthorizationFilter
{
    private readonly Claim _claim = claim;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity!.IsAuthenticated)
        {
            context.Result = new StatusCodeResult(400);
            return;
        }

        if (!CustomAuthorize.ValidateUserClaims(context.HttpContext, _claim.Type, _claim.Value))
        {
            context.Result = new StatusCodeResult(403);
        }
    }
}