using Microsoft.AspNetCore.Mvc;
using Model.ModelClass;
using ViewModelAnd;
using System.Text;
using RoleBasedAndJWT;
using Bussiness.Interfaces;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace RoleBasedAndJWT
{
    public class AuthenticationController : Controller
    {
        private IAuthenticationInterface Authentication1;
        public AuthenticationController(IAuthenticationInterface authentication)
        {
            Authentication1 = authentication;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("TestIfApiWorks")]
        public Response TestIfApiWorks()
        {
            var Response = new Response { Message = "Ok", Status = "Ok Status" };
            return Response;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {

            var x = await Authentication1.Login(model);
            if (x == null)
            {
                return Unauthorized();
            }
            if (x.ErrorCode == ErrorCodeEnum.Ok)
            {
                return Ok(new
                {
                    Token = x.Token,
                    RefreshToken = x.RefreshToken,
                    Expiration = x.Expiration
                });
            }
            else
            {
                return Unauthorized();
            }

        }



        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            var x =  Authentication1.Register(model);

            if (x.Result.Message == "Error")
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = x.Result.Message });
            }
            else
            {
                return Ok(new Response { Status = "Success", Message = "User created successfully!" });
            }
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var x = await Authentication1.Register(model);
            if (x.Status == "Error")
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = x.Message });

            }
            else
            {
                return Ok(new Response { Status = "Success", Message = "User created successfully!" });
            }
        }
        


        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {
            var x = await Authentication1.RefreshToken(tokenModel);
            if (x.Status == false)
            {
                return BadRequest(x.Message);
            }
            else
            {

                return new ObjectResult(new
                {
                    accessToken = x.AccessToken,
                    refreshToken = x.RefreshToken
                });
            }
        }

     /// <summary>
     ///   [Authorize]
     /// </summary>
     /// <param name="username"></param>
     /// <returns></returns>
        [HttpPost]
        [Route("revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            var x = await Authentication1.Revoke(username);
            return NoContent();
        }

      /// <summary>
      ///  [Authorize]
      /// </summary>
      /// <returns></returns>
        [HttpPost]
        [Route("revoke-all")]
        public async Task<IActionResult> RevokeAll()
        {
            var x = await Authentication1.RevokeAll();
            return NoContent();
        }

        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var x = Authentication1.CreateToken(authClaims);
            return x;
        }

        public string GenerateRefreshToken()
        {
            var x = Authentication1.GenerateRefreshToken();
            return x;
                  }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var x = Authentication1.GetPrincipalFromExpiredToken(token);
            return x;

        }
    }
}
