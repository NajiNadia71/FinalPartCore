namespace WebApi.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ViewModelAnd;
using Bussiness.Services;
using Bussiness.Interfaces;
using Bussiness;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppSetting _appSettings;

    public JwtMiddleware(RequestDelegate next, IOptions<AppSetting> appSettings)
    {
        _next = next;
        _appSettings = appSettings.Value;
    }

    public async Task Invoke(HttpContext context, IUserInterface userService, IJwtUtils jwtUtils)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var userId = jwtUtils.ValidateJwtToken(token);
        var x = context;
        if (userId != null)
        {
            // attach user to context on successful jwt validation
            context.Items["User"] = userService.GetById(userId.Value.ToString());
        }

        await _next(context);
    }
}