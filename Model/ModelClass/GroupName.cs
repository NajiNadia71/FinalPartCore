using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Model.ModelClass;

public class GroupName
{
    [Column("Title")]
    public string? Title { get; set; }
    [Column("Description")]
    public string Description { get; set; }
    [Key]
    public int Id  { get; set; }
    public ICollection<RoleGroup> RoleGroups { get; } = new List<RoleGroup>();
    public ICollection<UserGroup> UserGroups { get; } = new List<UserGroup>();
}
