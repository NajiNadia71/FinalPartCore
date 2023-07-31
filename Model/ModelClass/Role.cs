using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Model.ModelClass;
  public  class Role
    {
    [Required]
    [Column("Title")]
    public string Title { get; set; }
    [Required]
    [Column("ControllerName")]
    public string ControllerName  { get; set; }
    [Required]
    [Column("ActionName")]
    public string ActionName  { get; set; }
    [Required,Key]
    [Column("Id")]
    public Guid Id { get; set; }
    public ICollection<RoleGroup> RoleGroups { get; } = new List<RoleGroup>();
    public ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
    
}  