using Bussiness.Interfaces;
using Bussiness;

namespace RoleBasedAndJWT.Attribute
{
 
    public static class RoleCheckHelper
    {
      
       
            public static ViewModelAnd.Response LogMessage(IRoleCRUDInterface roleCRUDInterface, string RoleName, string UserId)
        {
            var x = roleCRUDInterface.HasTheRole(RoleName, UserId);
            return x;
        }
    
           }


}