using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace Model.ModelClass;
    public class RoleGroup
    {
    [Required]
    public Guid Id { get; set; }
    [Column("RoleId")]
    public Guid RoleId { get; set; }
    [Column("GroupNameId")]
    public int  GroupNameId { get; set; }
    [ForeignKey("RoleId")]
    public Role Role { get; set; } = null!;
    [ForeignKey("GroupNameId")]
    public GroupName GroupName { get; set; } = null!;
}

