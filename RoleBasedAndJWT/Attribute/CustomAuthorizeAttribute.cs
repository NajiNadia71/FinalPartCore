using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using ViewModelAnd;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
    public sealed class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _role;

    public CustomAuthorizeAttribute(string role)
    {
        _role = role;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Check if the user is in the specified role.
        if (!context.HttpContext.User.IsInRole(_role))
        {
            // If not, set a ForbiddenResult.
            context.Result = new ForbidResult();
        }
    }
}

