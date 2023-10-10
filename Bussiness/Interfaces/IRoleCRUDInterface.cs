using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ModelClass;
using ViewModelAnd;

namespace Bussiness.Interfaces
{
    public interface IRoleCRUDInterface
    {
        #region GroupName
        public Response CreateGroupName(GroupName groupName);
        public Response DeleteGroupName(int id );
        public Response EditGroupName(GroupName groupName);
        public IQueryable<GroupName> GetAllGroupName();

        #endregion
        #region Users

        public ViewModelAnd.User GetUserWithAllRole(string userId);
       
        public IQueryable<ApplicationUser> GetAllUser();
        public IQueryable<Role> GetRoldesThatUserDosenHave(string userId);
        public Response CreateNewRoleForUser(Guid roleId, string userId);
        #endregion
        #region RoleGroup

        public Response DeleteRoleGroup(Guid id);
        public bool CreateRoleGroup(RoleGroup roleGroup);
        ///  public IQueryable<RoleGroup> GetAllRoleGroup();

        #endregion
        #region UserGroup
        public IQueryable<GroupName> GetGroupRolesThatUserDosentHave(string userId);
        public Response CreateNewGroupNameForUser(int groupNameId, string userId);
        public Response DeleteUserGroup(Guid id);
        public IQueryable<UserGroup> GetAllUserGroup();
        
        #endregion
        #region UserRole

        public Response DeleteUserRole(Guid id);
        public IQueryable<Model.ModelClass.UserRole> GetAllUserRole();

        #endregion

        
        public IQueryable<Role> GetAllRolesThatGroupRoleDosentHave(int groupNameId);
        public ViewModelAnd.GroupNameVM GetAllRoleGroupForGroupRole(int groupNameId);
        public IQueryable<GroupName> GetAllGroupNames();
        public Response CreateNewRoleToRoleGroup(Guid roleId, int groupNameId);

        public Response HasTheRole(string roleName, string UserId);
    }
}
