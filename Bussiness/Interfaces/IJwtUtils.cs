using Model.ModelClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Interfaces
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(ApplicationUser user);
        public int? ValidateJwtToken(string token);
    }
}
