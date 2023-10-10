using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Model.ModelClass;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using ViewModelAnd;
using Microsoft.AspNetCore.Authentication;
using Bussiness.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Migrations;
using Microsoft.AspNetCore.Rewrite;

namespace Bussiness
{
    public class AuthenticationService: IAuthenticationInterface
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public AuthenticationService(ApplicationDbContext context,UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<LoginResponseViewModel> LoginOnlyJWTtokenAndRefreshToken([FromBody] LoginModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                    var token = CreateToken(authClaims);
                    var refreshToken = GenerateRefreshToken();
                    _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);
                    var xsss = await _userManager.UpdateAsync(user);

                    return (new LoginResponseViewModel
                    {

                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        RefreshToken = refreshToken,
                        Expiration = token.ValidTo,
                        ErrorCode = ErrorCodeEnum.Authorize,
                    });

                }
                else
                {
                    return (new LoginResponseViewModel { ErrorCode = ErrorCodeEnum.NotAuthorize });
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                throw new Exception(message);
            }
        
        }
        public async Task<LoginResponseViewModel> Login([FromBody] LoginModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var Roles = new List<string>();
                    var GroupRoles = new List<UserGroup>();
                    ////Here
                    ///
                    /// await _userManager.AddToRoleAsync(user, UserRoles.User);
                    // var AllRoles = await _roleManager.Roles.Select(x => x).ToListAsync();

                    //  IdentityResult roleResult;
                    //  var R= AllRoles.FirstOrDefault();
                    // var existed = await _roleManager.FindByNameAsync(R.Name);
                    // var existeds = await _roleManager.FindByIdAsync(R.Id);
                    // var existedggs = await _roleManager.RoleExistsAsync(R.Name);
                    //foreach (var role in AllRoles)
                    //{
                    //    bool RoleExists = await _roleManager.RoleExistsAsync(role.Name);
                    //    if (!RoleExists)
                    //    {

                    //        roleResult = await _roleManager.CreateAsync(new IdentityRole(R.Name));
                    //    }
                    //}
                    var userRoles = await GetRolesAsync2(user);

                
                    var userEspecialRoles = _context.UserRoles.Include(i => i.Role).Where(i => i.UserId == user.Id).ToList();
                    var NameOfRole = userEspecialRoles.Select(i => i.Role.Title).FirstOrDefault();
                    var userGroupRoles = _context.UserGroups.Include(i => i.GroupName).Where(i => i.UserId == user.Id).ToList();
                    var RolesInRoleGroups = _context.RoleGroups.Select(i => i.Role.Title).ToList();
                    var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                    foreach (var item in userRoles)
                    {
                        Roles.Add(item);
                        _userManager.AddToRoleAsync(user,item);
                        authClaims.Add(new Claim(ClaimTypes.Role, item));
                    }
                    //foreach (var userRole in userEspecialRoles)
                    //{
                    //    authClaims.Add(new Claim(ClaimTypes.Role, userRole.Role.Title));
                    //    _userManager.AddToRoleAsync(user, userRole.Role.Title);
                    //    Roles.Add(userRole);
                    //}
                    //foreach (var userGroup in RolesInRoleGroups)
                    //{
                    //    authClaims.Add(new Claim(ClaimTypes.Role, userGroup.ToString()));
                    //    _userManager.AddToRoleAsync(user, userGroup.ToString());

                    //}
                    //foreach (var group in userGroupRoles)
                    //{

                    //    GroupRoles.Add(group);
                    //}

                    var token = CreateToken(authClaims);
                    var refreshToken = GenerateRefreshToken();

                    _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

                  var xsss= await _userManager.UpdateAsync(user);

                    return (new LoginResponseViewModel
                    {
                   
                      ///  UserGroups = GroupRoles,
                        UserRoles = Roles,
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        RefreshToken = refreshToken,
                        Expiration = token.ValidTo,
                        ErrorCode = ErrorCodeEnum.Authorize,
                    });
                }
                return (new LoginResponseViewModel { ErrorCode = ErrorCodeEnum.NotAuthorize });
            }
            catch(Exception ex)
            {
                var message = ex.Message;//+ " " + ex.InnerException!=null?ex.InnerException.Message:"";
                throw new Exception(message);
            }
        }

        public async Task<IList<string>> GetRolesAsync2(ApplicationUser user)
        {
           
            try
            {
                var listOfRole=new List<string>();
                var userItem = _context.ApplicationUser.Where(i => i.Id == user.Id).FirstOrDefault();
                    var userEspecialRoles = _context.UserRoles.Include(i => i.Role).Where(i => i.UserId == user.Id).ToList();
                    var RolesInRoleGroups = _context.RoleGroups.Select(i => i.Role.Title).ToList();
                   // var authClaims = new List<Claim>{};

                    foreach (var userRole in userEspecialRoles)
                    {
                    //authClaims.Add(new Claim(ClaimTypes.Role, userRole.Role.Title));
                    ///  _userManager.AddToRoleAsync(userItem, userRole.Role.Title);
                    listOfRole.Add(userRole.Role.Title);


                    }
                    foreach (var userGroup in RolesInRoleGroups)
                    {
                    //  authClaims.Add(new Claim(ClaimTypes.Role, userGroup.ToString()));
                    //  _userManager.AddToRoleAsync(userItem, userGroup.ToString());
                    listOfRole.Add(userGroup);
                }
                return listOfRole;


            }
            catch (Exception ex)
            {

              
                return null;
            }
        }
        public async Task<Response> Register([FromBody] RegisterModel model)
        {
            try
            {
                var userExists = await _userManager.FindByNameAsync(model.Username);
                if (userExists != null)
                    return (

                        new Response { Status = "Error", Message = "User already exists!" });

                ApplicationUser user = new()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Username,
                    IsActive = true,
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                    return (new Response
                    {
                        ErrorCode = ErrorCodeEnum.Status500InternalServerError,
                        Status = "Error",
                        Message = "User creation failed! Please check user details and try again."
                    }
                        );

                return (new Response { Status = "Success", Message = "User created successfully!", ErrorCode = ErrorCodeEnum.Ok });

            }
            catch(Exception ex)
            {
                var message = ex.Message + " " + ex.InnerException.Message;
                return (new Response { Status = "Success", Message = message, ErrorCode = ErrorCodeEnum.Ok });

            }
        }

       
        public async Task<Response> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return (new Response{ Status = "Error", Message = "User already exists!", ErrorCode = ErrorCodeEnum.Status500InternalServerError });

            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return (new Response {ErrorCode=ErrorCodeEnum.Status500InternalServerError, Status = "Error", Message = "User creation failed! Please check user details and try again." });

            //if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            //    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            //if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            //    await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            //if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            //{
            //    await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            //}
            //if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            //{
            //    await _userManager.AddToRoleAsync(user, UserRoles.User);
            //}
            return (new Response { Status = "Success", Message = "User created successfully!",ErrorCode = ErrorCodeEnum.Ok });
        }

       
        public async Task<LoginResponseViewModel> RefreshToken(TokenModel tokenModel)
        {
            if (tokenModel is null)
            {
                return (new LoginResponseViewModel { ErrorCode = ErrorCodeEnum.InvalidClientRequest , Message = "Invalid client request" }); //BadRequest("Invalid client request");
            }

            string? accessToken = tokenModel.AccessToken;
            string? refreshToken = tokenModel.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return (new LoginResponseViewModel { ErrorCode = ErrorCodeEnum.InvalidClientRequest, Status=false, Message= "Invalid access token or refresh token" }); ///  return BadRequest("Invalid access token or refresh token");
            }

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string username = principal.Identity.Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            var user = await _userManager.FindByNameAsync(username);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return (new LoginResponseViewModel { ErrorCode = ErrorCodeEnum.InvalidAccessTokenOrRefreshToken , Status = false, Message = "Invalid access token or refresh token" }); //BadRequest("Invalid access token or refresh token");
            }

            var newAccessToken = CreateToken(principal.Claims.ToList());
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return  (new LoginResponseViewModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken
            });
        }

        
        public async Task<Response> Revoke(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return (new Response { ErrorCode = ErrorCodeEnum.BadRequest, Message = "Invalid user name" });
                  
            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);

            return (new Response { ErrorCode = ErrorCodeEnum.NoContent, Message = "NoContent" });// NoContent();
        }

       
        public async Task<Response> RevokeAll()
        {
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }

            return (new Response { ErrorCode = ErrorCodeEnum.NoContent, Message = "NoContent" });// NoContent();
        }

        public JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        public  string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }

    }
}
