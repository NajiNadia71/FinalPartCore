using Bussiness.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model;
using Model.ModelClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ViewModelAnd;
using ViewModelAnd.ViewModel;


namespace Bussiness.Services
{
    public class UserService : IUserInterface
    {
        private readonly ApplicationDbContext _context;
        private IJwtUtils _jwtUtils;
        private readonly AppSetting _appSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService(ApplicationDbContext context, IJwtUtils jwtUtils, IOptions<AppSetting> appSettings, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }
        public bool IsUserCredentialOk(ApplicationUser user, string password)
        {

            var result = _userManager.CheckPasswordAsync(user, password).Result;
            return result;
        }

        public AuthenticateResponse Authenticate(LoginModel model)
        {
            // var user = _context.Users.SingleOrDefault(x => x.Username == model.Username);
            // validate
            /// if (user == null || !BCrypt.Verify(model.Password, user.PasswordHash))
            /// 
            var user = _context.Users.Where(x => x.UserName == model.Username).FirstOrDefault();
            var IsOk = IsUserCredentialOk(user, model.Password);
            throw new AppException("Username or password is incorrect");

            // authentication successful so generate jwt token
            var jwtToken = _jwtUtils.GenerateJwtToken(user);

            return new AuthenticateResponse(user, jwtToken);
        }
      

        public IEnumerable<User> GetAll()
        {
            return _context.Users.Select(i =>
                new ViewModelAnd.User
                {
                    Email = i.Email,
                    Name = i.UserName,
                    ///  UserRoles=i.
                }).AsEnumerable();
        }

        public User GetById(string id)
        {
            var user = _context.Users.Find(id);
            var X=    new ViewModelAnd.User
                {
                    Email = user.Email,
                    Name = user.UserName,
                    ///  UserRoles=i.
                };
            if (user == null) throw new KeyNotFoundException("User not found");
            return X;
        }


    }
}
