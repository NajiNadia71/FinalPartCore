namespace WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Model.ModelClass;
using ViewModelAnd;
using WebApi.Authorization;
using Model;
using Bussiness;
using Bussiness.Interfaces;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private IUserInterface _userService;

    public UsersController(IUserInterface userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("[action]")]
    public IActionResult Authenticate(LoginModel model)
    {
        var response = _userService.Authenticate(model);
        return Ok(response);
    }

    // [Authorize(Role.Admin)]
    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(string id)
    {
        // only admins can access other user records
        var currentUser = (User)HttpContext.Items["User"];
        if (id != currentUser.Id )//&& currentUser.Role != Role.Admin)
            return Unauthorized(new { message = "Unauthorized" });

        var user = _userService.GetById(id);
        return Ok(user);
    }
}