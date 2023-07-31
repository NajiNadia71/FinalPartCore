using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.ModelClass;
using System.Xml;
using System.Data.Entity;
using System.Reflection.Metadata;
using System.Data.Common;
using Microsoft.AspNetCore.Identity;

namespace Model
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "E8015CA3-DB29-46AB-95DF-04E484D78835", ConcurrencyStamp = "", Name = "GetAllGroupRoleNames", NormalizedName = "" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "2A18E626-6A6F-4755-BB1A-06E3BD741519", ConcurrencyStamp = "", Name = "EditRoleGroupd", NormalizedName = "" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "B1C8B66B-7B9A-4C72-A758-08B95A348188", ConcurrencyStamp = "", Name = "AddRoleToRoleGroup", NormalizedName = "" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "3F781779-E0A0-4245-8183-1837B7554A3D", ConcurrencyStamp = "", Name = "GetAllUsers", NormalizedName = "" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "E0967BB8-DCB8-441D-BB3F-3628BC743302", ConcurrencyStamp = "", Name = "AddRoleGroupForUser", NormalizedName = "" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "BB109ED0-2183-47A7-BC7B-464047400115", ConcurrencyStamp = "", Name = "CreateRoleGroup", NormalizedName = "" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "3BD071B4-EBA4-4C47-845E-4F255BD8C4C0", ConcurrencyStamp = "", Name = "DeleteRoleFromRoleGroup", NormalizedName = "" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "FE84485D-7F8D-43E0-B069-5157BBB68F0F", ConcurrencyStamp = "", Name = "CreateRoleGroupd", NormalizedName = "" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "E1F01805-25A1-478F-80FF-5338B379840E", ConcurrencyStamp = "", Name = "EditRoleGroup", NormalizedName = "" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "1EA03C12-3EEE-43D1-B9C6-5EB06321A130", ConcurrencyStamp = "", Name = "GetAllRolesOfGroupRole", NormalizedName = "" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "F18B19B5-962F-4E1E-A484-882BF1FE398E", ConcurrencyStamp = "", Name = "DeleteRoleGroup", NormalizedName = "" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "7E3A93C4-BFA4-4098-8742-9185B123DC31", ConcurrencyStamp = "", Name = "DeleteRolesForUser", NormalizedName = "" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "3FFBD3F9-B616-4164-8EA7-A473EBC4B1BD", ConcurrencyStamp = "", Name = "GetAllRolesGroupsForUser", NormalizedName = "" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "E1B4457B-2E32-431D-8A0A-A8CD59828821", ConcurrencyStamp = "", Name = "DeleteRoleGroupForUser", NormalizedName = "" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "030FB67C-A050-4D47-98A8-C857DBA89576", ConcurrencyStamp = "", Name = "GetUserInfo", NormalizedName = "" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "6AF8DEB0-DEC7-4BD5-A734-D5AF20380857", ConcurrencyStamp = "", Name = "AddRolesForUser", NormalizedName = "" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "7D5CBCE5-CBC1-4DC4-9F94-E394A2BFB7A9", ConcurrencyStamp = "", Name = "GetAllRolesForUser", NormalizedName = "" });

            //Property Configurations

            //builder.Entity<UserGroup>().Property(s => s.GroupName).HasColumnName("GroupNameId").IsRequired();

        }

        public Microsoft.EntityFrameworkCore.DbSet<ApplicationUser> ApplicationUser { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<GroupName> GroupNames { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Role> Roles { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<RoleGroup> RoleGroups { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<UserGroup> UserGroups { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<UserRole> UserRoles { get; set; }


    }
}
