using Bussiness.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Model;
using Model.ModelClass;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelAnd;

namespace Bussiness
{
    public class RoleCheckHelper : IRoleCheckHelper
    {
        public Response HasTheRole(string roleName, string useId)
        {
            var connectionstring = "Data Source=NN\\SQLEXPRESS;Initial Catalog=RoleBasedAndJWTDB;User ID=Admin;Password=123456;TrustServerCertificate=True";
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionstring);
            using (ApplicationDbContext _context = new ApplicationDbContext(optionsBuilder.Options))
            {
                try
                {

                    var userIdExists = _context.ApplicationUser.Where(i => i.Id == useId).Any();
                    if (!userIdExists)
                    {
                        return (new Response
                        {
                            ErrorCode = ErrorCodeEnum.DosentExiteInDatabase,
                        });
                    }

                    else
                    {
                        var roleId = _context.Roles.Where(i => i.Title == roleName).Select(i => i.Id).FirstOrDefault();
                        var HasRoleInUserRoles = _context.UserRoles.Where(i => i.UserId == useId && i.RoleId == roleId).Any();
                        if (HasRoleInUserRoles)
                            return (new Response
                            {
                                ErrorCode = ErrorCodeEnum.Ok,
                            }
                                );
                        else
                        {
                            return (new Response
                            {
                                ErrorCode = ErrorCodeEnum.NotAuthorize,
                            }
                              );
                        }
                    }



                }
                catch (Exception ex)
                {
                    var message = ex.Message + " " + ex.InnerException.Message;
                    return (new Response { Status = "Success", Message = message, ErrorCode = ErrorCodeEnum.Ok });

                }
            }

        }
    }
    }
//    public class InHurry
//    {
//        public Response my()
//        { 
       
//}




//public Response HasTheRoleCO(string roleName, string UserId)
//        {
//            using (var _context = new ApplicationDbContext(_contextOptions))
//            {
//                try
//                {

//                    var userIdExists = _context.ApplicationUser.Where(i => i.Id == UserId).Any();
//                    if (!userIdExists)
//                    {
//                        return (new Response
//                        {
//                            ErrorCode = ErrorCodeEnum.DosentExiteInDatabase,
//                        });
//                    }

//                    else
//                    {
//                        var roleId = _context.Roles.Where(i => i.Title == roleName).Select(i => i.Id).FirstOrDefault();
//                        var HasRoleInUserRoles = _context.UserRoles.Where(i => i.UserId == UserId && i.RoleId == roleId).Any();
//                        if (HasRoleInUserRoles)
//                            return (new Response
//                            {
//                                ErrorCode = ErrorCodeEnum.Ok,
//                            }
//                                );
//                        else
//                        {
//                            return (new Response
//                            {
//                                ErrorCode = ErrorCodeEnum.NotAuthorize,
//                            }
//                              );
//                        }
//                    }



//                }
//                catch (Exception ex)
//                {
//                    var message = ex.Message + " " + ex.InnerException.Message;
//                    return (new Response { Status = "Success", Message = message, ErrorCode = ErrorCodeEnum.Ok });

//                }
//            }
//        }
//      //  public  ApplicationDbContext _context;

//       // public InHurry(ApplicationDbContext context)
//       // {
//      //      _context = context;
//      //  }
//        //public Response HasTheRoleCO(string roleName, string UserId)
//        //{
          
//        //}
//    }
//}
