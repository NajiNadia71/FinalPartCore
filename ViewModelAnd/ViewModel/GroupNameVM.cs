using System.Collections.Generic;

namespace ViewModelAnd
{
    public class GroupNameVM
    {
        public string RoleId { get; set; }
        public string? Title { get; set; }

        public string Description { get; set; }

        public int Id { get; set; }
        public List<RoleGroupVM> RoleGroupVMs { get; set; }
    }
    public class RoleGroupVM
    {
        public string GroupNameTitle { get; set; }
        public int GroupNameId { get; set; }

        public Guid RoleId { get; set; }

        public Guid Id { get; set; }
        public string RoleITitle { get; set; }
    }
}
