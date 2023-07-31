using Microsoft.AspNetCore.Identity;
namespace Model.ModelClass
{
    public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public ICollection<UserGroup> UserGroups { get; } = new List<UserGroup>();
        public ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
        public bool IsActive { get; set; }
        
    }
}
