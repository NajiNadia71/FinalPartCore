using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelAnd;

namespace Bussiness
{
    public interface IRoleCheckHelper
    {
        public Response HasTheRole(string roleName, string UserId);
    }
}
