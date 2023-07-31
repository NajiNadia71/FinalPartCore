using Model.ModelClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelAnd.ViewModel
{
    public  class AuthenticateResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public Role Role { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(ApplicationUser user, string token)
        {
            Id = user.Id;
            FirstName = user.UserName;
         
            ///Role = user.;
            Token = token;
        }
    }
}
