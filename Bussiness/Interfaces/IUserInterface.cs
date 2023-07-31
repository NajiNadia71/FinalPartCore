using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.Extensions.Options;

///using WebApi.Entities;
using ViewModelAnd;
using ViewModelAnd.ViewModel;

namespace Bussiness.Interfaces
{
    public interface IUserInterface
    {
        public AuthenticateResponse Authenticate(LoginModel model);
        public IEnumerable<User> GetAll();
        public User GetById(string id);
    }
}
