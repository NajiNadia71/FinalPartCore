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
using System.Text;
using ViewModelAnd;

namespace Bussiness.Interfaces
{
    public interface IAuthenticationInterface
    {
        public  Task<IList<string>> GetRolesAsync2(ApplicationUser user);
        public  Task<LoginResponseViewModel> Login([FromBody] LoginModel model);
        public  Task<Response> Register([FromBody] RegisterModel model);
        
        public  Task<Response> RegisterAdmin([FromBody] RegisterModel model);
        public  Task<LoginResponseViewModel> RefreshToken(TokenModel tokenModel);
        public  Task<Response> Revoke(string username);
        public  Task<Response> RevokeAll();
        public JwtSecurityToken CreateToken(List<Claim> authClaims);
        public  string GenerateRefreshToken();
        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
       



    }
}
