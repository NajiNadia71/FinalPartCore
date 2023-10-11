using Bussiness.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model;
using Model.ModelClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelAnd;

namespace Bussiness.Services
{
    public class RoleCRUD:IRoleCRUDInterface
    {
        #region General
        private readonly ILogger<RoleCRUD> _logger;
        private readonly ApplicationDbContext _context;

        public RoleCRUD(ApplicationDbContext context, ILogger<RoleCRUD> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into HomeController");
        }
       
        #endregion
        #region GroupName
        public Response CreateGroupName(GroupName groupName)
        { 
            var res=new Response();
            try
            {
                var item = _context.GroupNames.Where(i => i.Title==groupName.Title).FirstOrDefault();
                if (item==null)
                {
                    _context.GroupNames.Add(groupName);
                    _context.SaveChanges();
                    res.Status = ErrorCodeEnum.Save.ToString();

                    return res;
                }
                else
                {
                    _logger.LogDebug("there is Same Title {id}", item.Id);
                    res.Status = ErrorCodeEnum.Status500InternalServerError.ToString();

                    return res;

                }
            }
            catch (Exception ex)
            {
                res.Status = ErrorCodeEnum.Status500InternalServerError.ToString();

                _logger.LogDebug("Some Error in Create  Of : {id} and Error Message Of {message}", groupName.Title,ex.Message); return res;
            }
        }
       
        public Response DeleteGroupName(int id)
        {
            var x = new Response();
            try {
                var item = _context.GroupNames.Where(i => i.Id == id).FirstOrDefault();
                if (item != null)
                {
                    _context.GroupNames.Remove(item);
                    _context.SaveChanges();
                    x.ErrorCode = ErrorCodeEnum.Save;
                    return x;
                }
                else
                {
                    _logger.LogDebug("there is no Data for The Id  Of {id}", id);
                    x.ErrorCode = ErrorCodeEnum.Status500InternalServerError;
                    return x;

                }
            }
            catch (Exception ex) {

                _logger.LogDebug( "Some Error in Delete with Id Of : {id} and Error Message Of {message}",id, ex.Message);
                x.ErrorCode = ErrorCodeEnum.Status500InternalServerError;
                return x;
            }
        }

        public Response EditGroupName(GroupName groupName)
        {
            var x = new Response();
            try
            {
                var item = _context.GroupNames.Where(i => i.Id == groupName.Id).FirstOrDefault();
                if (item != null)
                {
                    item.Title = groupName.Title;
                    item.Description = groupName.Description;
                    
                    _context.SaveChanges();
                    x.ErrorCode = ErrorCodeEnum.Save;
                    return x;
                }
                else
                {
                    _logger.LogDebug("there is no Data for The Id  Of {id}", groupName.Id);
                    x.ErrorCode = ErrorCodeEnum.Status500InternalServerError;
                    return x;

                }
            }
            catch (Exception ex)
            {
                x.ErrorCode = ErrorCodeEnum.Status500InternalServerError;
               
                _logger.LogDebug("Some Error in Delete with Id Of : {id} and Error Message Of {message}", groupName.Id, ex.Message);
                return x;
            }
        }

        public IQueryable<GroupName> GetAllGroupName()
        {
            try
            {
                return _context.GroupNames.AsQueryable();
              
            }
            catch (Exception ex)
            {

                _logger.LogDebug("Some Error in Get All Data with Error Message Of {message}",  ex.Message);
                throw new Exception (ex.Message.ToString());
            }
        }

        #endregion
        #region RoleGroup
        public Response DeleteRoleGroup(Guid id)
        {
            var x = new Response();
            try
            {
                var item = _context.RoleGroups.Where(i => i.Id == id).FirstOrDefault();
                if (item != null)
                {
                    _context.RoleGroups.Remove(item);
                    _context.SaveChanges();
                    x.ErrorCode = ErrorCodeEnum.Save;
                    return x;
                }
                else
                {
                    _logger.LogDebug("there is no Data for The Id  Of {id}", id);
                    x.ErrorCode = ErrorCodeEnum.Status500InternalServerError;
                    return x;

                }
            }
            catch (Exception ex)
            {

                _logger.LogDebug("Some Error in Delete with Id Of : {id} and Error Message Of {message}", id, ex.Message);
                x.ErrorCode = ErrorCodeEnum.Status500InternalServerError;
                return x;
            }
        }
        public bool CreateRoleGroup(RoleGroup roleGroup)
        {
            try
            {
                var item = _context.RoleGroups.Where(i => i.RoleId == roleGroup.RoleId && 
                i.GroupNameId==roleGroup.GroupNameId).FirstOrDefault();
                if (item != null)
                {
                    item.RoleId = roleGroup.RoleId;
                    item.GroupNameId = roleGroup.GroupNameId;
                    _context.Add(item);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    _logger.LogDebug("there is no Data for The Id  Of {id}", roleGroup.Id);
                    return false;

                }
            }
            catch (Exception ex)
            {

                _logger.LogDebug("Some Error in Delete with Id Of : {id} and Error Message Of {message}", roleGroup.Id, ex.Message); return false;
            }
        }


        #endregion
        #region User
        public IQueryable<ApplicationUser> GetAllUser()
        {
            try
            {
              
                return _context.ApplicationUser.AsQueryable();

            }
            catch (Exception ex)
            {

                _logger.LogDebug("Some Error in Get All Data with Error Message Of {message}", ex.Message);
                throw new Exception(ex.Message.ToString());
            }
        }
        #region RolesForUser
        public ViewModelAnd.User GetUserWithAllRole(string userId)
        {
            try
            {

                var UserRoles = new List<ViewModelAnd.UserRole>();
                var UserGroupRoleNames = new List<ViewModelAnd.UserGroupRoleName>();
                var res = GetAllUser();//.Where(i => i.Id == userId).Include(i => i.UserRoles).ThenInclude(i => i.Role)
                var x = res.Where(i => i.Id == userId).Include(i => i.UserRoles).ThenInclude(i => i.Role).Include(i => i.UserGroups).ThenInclude(j => j.GroupName)
                    .Select(i => new ViewModelAnd.User
                    {
                        Name = i.UserName,
                        Email = i.Email,
                        Id = i.Id,
                        UserRoles = i.UserRoles.Count() != -0 ? i.UserRoles.Select(j => new ViewModelAnd.UserRole
                        {
                            UserRoleId = j.Id,
                            RoleName = j.Role.Title,
                            RoleId = j.RoleId

                        }).ToList() : new List<ViewModelAnd.UserRole>(),

                        UserGroupRoleNames = i.UserGroups.Count != 0 ? i.UserGroups.Select(o => new ViewModelAnd.UserGroupRoleName
                        {
                            UserGroupRoleNameId = o.Id,
                            RoleGroupId = o.GroupNameId,
                            RoleGroupName = o.GroupName.Title,

                        }).ToList() : new List<ViewModelAnd.UserGroupRoleName>(),

                    }).FirstOrDefault();
                return x;
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Some Error in Get All Role for User with Error Message Of {message}", ex.Message);
                throw new Exception(ex.Message.ToString());
            }

        }
        public IQueryable<Role> GetRoldesThatUserDosenHave(string userId)
        {
            try
            {
               var listOfRoles= (from s in _context.Roles
                where !_context.UserRoles.Any(es => (es.RoleId == s.Id) && (es.UserId ==userId))
                select s).AsQueryable();
                
                return listOfRoles;
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Some Error in Get All Role for User with Error Message Of {message}", ex.Message);
                throw new Exception(ex.Message.ToString());
            }

        }

        public Response CreateNewRoleForUser(Guid roleId, string userId)
        {
            try
            {
                var res = new Response();
                var user = _context.ApplicationUser.Where(i => i.Id == userId).FirstOrDefault();
                if (user == null)
                {
                    res.ErrorCode = ErrorCodeEnum.DosentExiteInDatabase;
                    return res;
                }
                var Role = _context.Roles.Where(i => i.Id == roleId).FirstOrDefault();
                if (Role == null)
                {
                    res.ErrorCode = ErrorCodeEnum.DosentExiteInDatabase;
                    return res;
                }
                else
                {
                    var userRole=new Model.ModelClass.UserRole();
                    userRole.RoleId = roleId;
                    userRole.UserId =userId;
                    userRole.Id = Guid.NewGuid();
                    _context.UserRoles.Add(userRole);
                    _context.SaveChanges();
                    res.ErrorCode = ErrorCodeEnum.Save;
                    return res;
                }
              
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Some Error in Get All Role for User with Error Message Of {message}", ex.Message);
                throw new Exception(ex.Message.ToString());
            }
        }

        public Response DeleteUserRole(Guid id)
        {
            var x = new Response();
            try
            {

                var item = _context.UserRoles.Where(i => i.Id == id).FirstOrDefault();
                if (item != null)
                {
                    _context.UserRoles.Remove(item);
                    _context.SaveChanges();
                    x.ErrorCode = ErrorCodeEnum.Save;
                    return x;
                }
                else
                {
                    _logger.LogDebug("there is no Data for The Id  Of {id}", id);
                    x.ErrorCode = ErrorCodeEnum.Status500InternalServerError;
                    return x;

                }
            }
            catch (Exception ex)
            {

                _logger.LogDebug("Some Error in Delete with Id Of : {id} and Error Message Of {message}", id, ex.Message);
                x.ErrorCode = ErrorCodeEnum.Status500InternalServerError;
                return x;

            }
        }
        #endregion

        #region GroupRolesForUser
        public IQueryable<GroupName> GetGroupRolesThatUserDosentHave(string userId)
        {
            try
            {
                var listOfGroupName = (from s in _context.GroupNames
                                   where !_context.UserGroups.Any(es => (es.GroupNameId == s.Id) && (es.UserId == userId))
                                   select s).AsQueryable();

                return listOfGroupName;
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Some Error in Get All Role for User with Error Message Of {message}", ex.Message);
                throw new Exception(ex.Message.ToString());
            }

        }

        public Response CreateNewGroupNameForUser(int groupNameId, string userId)
        {
            try
            {
                var res = new Response();
                var user = _context.ApplicationUser.Where(i => i.Id == userId).FirstOrDefault();
                if (user == null)
                {
                    res.ErrorCode = ErrorCodeEnum.DosentExiteInDatabase;
                    return res;
                }
                var Role = _context.GroupNames.Where(i => i.Id == groupNameId).FirstOrDefault();
                if (Role == null)
                {
                    res.ErrorCode = ErrorCodeEnum.DosentExiteInDatabase;
                    return res;
                }
                else
                {
                    var userGroup = new Model.ModelClass.UserGroup();
                    userGroup.GroupNameId = groupNameId;
                    userGroup.UserId = userId;
                    userGroup.Id = Guid.NewGuid();
                    _context.UserGroups.Add(userGroup);
                    _context.SaveChanges();
                    res.ErrorCode = ErrorCodeEnum.Save;
                    return res;
                }

            }
            catch (Exception ex)
            {
                _logger.LogDebug("Some Error in Get All Role for User with Error Message Of {message}", ex.Message);
                throw new Exception(ex.Message.ToString());
            }
        }

        public Response DeleteUserGroup(Guid id)
        {
            var x = new Response();
            try
            {

                var item = _context.UserGroups.Where(i => i.Id == id).FirstOrDefault();
                if (item != null)
                {
                    _context.UserGroups.Remove(item);
                    _context.SaveChanges();
                    x.ErrorCode = ErrorCodeEnum.Save;
                    return x;
                }
                else
                {
                    _logger.LogDebug("there is no Data for The Id  Of {id}", id);
                    x.ErrorCode = ErrorCodeEnum.Status500InternalServerError;
                    return x;

                }
            }
            catch (Exception ex)
            {

                _logger.LogDebug("Some Error in Delete with Id Of : {id} and Error Message Of {message}", id, ex.Message);
                x.ErrorCode = ErrorCodeEnum.Status500InternalServerError;
                return x;

            }
        }
        #endregion
        #endregion
        #region UserGroup
        public IQueryable<UserGroup> GetAllUserGroup()
        {
            try
            {
                return _context.UserGroups.AsQueryable();

            }
            catch (Exception ex)
            {

                _logger.LogDebug("Some Error in Get All Data with Error Message Of {message}", ex.Message);
                throw new Exception(ex.Message.ToString());
            }
        }
        //public bool DeleteUserGroup(Guid id)
        //{
        //    try
        //    {
               
        //        var item = _context.UserGroups.Where(i => i.Id == id).FirstOrDefault();
        //        if (item != null)
        //        {
        //            _context.UserGroups.Remove(item);
        //            _context.SaveChanges();
        //            return true;
        //        }
        //        else
        //        {
        //            _logger.LogDebug("there is no Data for The Id  Of {id}", id);
        //            return false;

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        _logger.LogDebug("Some Error in Delete with Id Of : {id} and Error Message Of {message}", id, ex.Message); return false;
        //    }
        //}
    
        #endregion
        #region UserRole
    

        public IQueryable<Model.ModelClass.UserRole> GetAllUserRole()
        {
            try
            {
                return _context.UserRoles.AsQueryable();

            }
            catch (Exception ex)
            {

                _logger.LogDebug("Some Error in Get All Data with Error Message Of {message}", ex.Message);
                throw new Exception(ex.Message.ToString());
            }
        }

        #endregion

        public IQueryable<GroupName> GetAllGroupNames()
        {
            return _context.GroupNames.AsQueryable();
        }
        public ViewModelAnd.GroupNameVM GetAllRoleGroupForGroupRole(int groupNameId)
        {
            try
            {

                var RoleGroupVMs = new List<ViewModelAnd.RoleGroupVM>();
                var res = GetAllGroupNames();
                var x = res.Where(i => i.Id == groupNameId).Include(i => i.RoleGroups)
                    .Select(i => new ViewModelAnd.GroupNameVM
                    {
                        Description = i.Description,
                        Title = i.Title,
                        Id = i.Id,
                        RoleGroupVMs = i.RoleGroups.Count() != -0 ? i.RoleGroups.Select(j => new ViewModelAnd.RoleGroupVM
                        {
                            Id = j.Id,
                            RoleITitle = j.Role.Title,
                            RoleId = j.RoleId

                        }).ToList() : new List<ViewModelAnd.RoleGroupVM>(),

                    }).FirstOrDefault();
                return x;
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Some Error in Get All Role for User with Error Message Of {message}", ex.Message);
                throw new Exception(ex.Message.ToString());
            }
        }
        public IQueryable<Role> GetAllRolesThatGroupRoleDosentHave(int groupNameId)
        {
            try
            {
                var listOfRoles = (from s in _context.Roles
                                   where !_context.RoleGroups.Any(es => (es.RoleId == s.Id) && (es.GroupNameId == groupNameId))
                                   select s).AsQueryable();

                return listOfRoles;
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Some Error in Get All Role for groupNameId with Error Message Of {message}", ex.Message);
                throw new Exception(ex.Message.ToString());
            }
        }


        public Response CreateNewRoleToRoleGroup(Guid roleId,int groupNameId)
        {
            try
            {
                var res = new Response();
                var user = _context.GroupNames.Where(i => i.Id == groupNameId).FirstOrDefault();
                if (user == null)
                {
                    res.ErrorCode = ErrorCodeEnum.DosentExiteInDatabase;
                    return res;
                }
                var Role = _context.Roles.Where(i => i.Id == roleId).FirstOrDefault();
                if (Role == null)
                {
                    res.ErrorCode = ErrorCodeEnum.DosentExiteInDatabase;
                    return res;
                }
                else
                {
                    var RoleGroup = new Model.ModelClass.RoleGroup();
                    RoleGroup.RoleId = roleId;
                    RoleGroup.GroupNameId = groupNameId;
                    RoleGroup.Id = Guid.NewGuid();
                    _context.RoleGroups.Add(RoleGroup);
                    _context.SaveChanges();
                    res.ErrorCode = ErrorCodeEnum.Save;
                    return res;
                }

            }
            catch (Exception ex)
            {
                _logger.LogDebug("Some Error in add Role in GroupRole with Error Message Of {message}", ex.Message);
                throw new Exception(ex.Message.ToString());
            }
        }
        public Response HasTheRole(string roleName, string UserId)
        {
            try
            {
                var userIdExists = _context.ApplicationUser.Where(i=>i.Id== UserId).Any();
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
            catch (Exception ex)
            {
                var message = ex.Message + " " + ex.InnerException.Message;
                return (new Response { Status = "Success", Message = message, ErrorCode = ErrorCodeEnum.Ok });

            }
        }
    }
}
