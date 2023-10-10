using Bussiness.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelAnd;

namespace Bussiness
{
    public  class RoleCheckHelper:IRoleCheckHelper
    {
    
        public  Response HasTheRole(string roleName, string UserId)
        {
            try
            {
                using (var _context = new Model.ApplicationDbContext())
                {
                    var userIdExists = _context.ApplicationUser.Where(i => i.Id == UserId).Any();
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
                        var HasRoleInUserRoles = _context.UserRoles.Where(i => i.UserId == UserId && i.RoleId == roleId).Any();
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
               

            }
            catch (Exception ex)
            {
                var message = ex.Message + " " + ex.InnerException.Message;
                return (new Response { Status = "Success", Message = message, ErrorCode = ErrorCodeEnum.Ok });

            }
        }
    }
}
