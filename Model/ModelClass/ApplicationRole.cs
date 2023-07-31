using Microsoft.AspNetCore.Identity;
namespace Model.ModelClass
{    public class ApplicationRole : IdentityRole
    {
       public Guid RoleId { get; set; }
    }
}
