using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace Model.ModelClass
{
    public class UserRole
    {
            [Key]
            public Guid Id { get; set; }
            [Column("UserId")]
            public string UserId { get; set; }
            [Column("RoleId")]
            public Guid RoleId { get; set; }
             [ForeignKey("RoleId")]
             public Role Role { get; set; } = null!;
             [ForeignKey("UserId")]
            public ApplicationUser ApplicationUser { get; set; } = null!;
         
    } }
