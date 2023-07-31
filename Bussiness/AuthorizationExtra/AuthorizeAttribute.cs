using Bussiness;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Model;
using Model.ModelClass;
using ViewModelAnd;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly IList<Role> _roles;
  //  private readonly ApplicationDbContext _context;
    public AuthorizeAttribute()
    {
      ///  _roles = roles ?? new Role[] { };
       // _context = context;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // skip authorization if action is decorated with [AllowAnonymous] attribute
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        // authorization
        var user = (User)context.HttpContext.Items["User"];
     
        //if (user == null || (_roles.Any() && !_roles.Contains(user.)))
        //{
        //    // not logged in or role not authorized
        //    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        //}
    }
}