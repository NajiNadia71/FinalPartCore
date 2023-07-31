using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace Model.ModelClass;

public class UserGroup
{

    [Key]
    public Guid Id { get; set; }
    [Column("UserId")] 
    public string UserId { get; set; }
    [Column("GroupNameId")]
    public int GroupNameId { get; set; }
    [ForeignKey("GroupNameId")]
    public GroupName GroupName { get; set; } = null!;
    [ForeignKey("UserId")]
    public ApplicationUser ApplicationUser { get; set; } = null!;
}

