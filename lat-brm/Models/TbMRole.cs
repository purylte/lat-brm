using System;
using System.Collections.Generic;

namespace lat_brm.Models
{
    public partial class TbMRole
    {
        public TbMRole()
        {
            TbMAccountRoles = new HashSet<TbMAccountRole>();
        }

        public string? Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid Guid { get; set; }

        public virtual ICollection<TbMAccountRole>? TbMAccountRoles { get; set; }
    }
}
