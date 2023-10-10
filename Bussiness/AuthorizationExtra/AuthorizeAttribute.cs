using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Bussiness.Interfaces;
using Bussiness.Services;

using ViewModelAnd;
using System.IdentityModel.Tokens.Jwt;
using Bussiness;
using Model;

public class CustomAuthorizeRoleAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _role;


    public CustomAuthorizeRoleAttribute(string role)
    {
        _role = role;
        

    }
 
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var CustomAuthorizeRoleAttribute = context.Filters[3] as CustomAuthorizeRoleAttribute;
        var Role= CustomAuthorizeRoleAttribute._role;
        var Cookies1 = context.HttpContext.Request.Cookies.TryGetValue("X-ErrorCode",out string? OutErrorCode);
        var Cookies2 = context.HttpContext.Request.Cookies.TryGetValue("X-Access-Token", out string? OutAccessToken);
        var Cookies3 = context.HttpContext.Request.Cookies.TryGetValue("X-Expiration", out string? OutExpiration);
        var Cookies4 = context.HttpContext.Request.Cookies.TryGetValue("X-Refresh-Token", out string? OutRefreshToken);

      
        var token = OutAccessToken;
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);

        var UserId = jwtSecurityToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
        var x = new RoleCheckHelper();
        var result= x.HasTheRole(Role, UserId);
       
        if (result.ErrorCode!=ErrorCodeEnum.Ok)
        {
            // If not, set a ForbiddenResult.

            context.Result = new ForbidResult();
        }
    }
}