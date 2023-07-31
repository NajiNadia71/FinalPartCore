using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelAnd
{
    public class LoginResponseViewModel
    {
        public string Subject { get; set; }
        public User user { get; set; }
        public string AccessToken { get;set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
        public ErrorCodeEnum ErrorCode { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
          public List<string> UserRoles { get; set; }
        public List<SigningCredentials> SigningCredentials { get; set; }
        public ClaimsIdentity ClaimsIdentity { get; set; }
        //public List<Model.ModelClass.UserRole> UserRoles { get; set; }
        //public List<Model.ModelClass.UserGroup> UserGroups { get; set; }
    }
}
