using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelAnd
{
    public class User
    {
     
        public string Id { get; set; }
        public string RoleId { get; set; }
        public int RoleGroupId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        //public List<UserRole> UserRoles { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public List<UserGroupRoleName> UserGroupRoleNames { get; set; }
    }
    public class UserRole
    {
        public Guid UserRoleId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string RoleName { get; set; }
        public Guid RoleId { get; set; }
    }
    public class UserGroupRoleName
    {
        public Guid UserGroupRoleNameId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string RoleGroupName { get; set; }
        public int RoleGroupId { get; set; }
    }
}
