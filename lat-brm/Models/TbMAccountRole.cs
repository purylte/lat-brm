using System;
using System.Collections.Generic;

namespace lat_brm.Models
{
    public partial class TbMAccountRole
    {
        public Guid? AccountGuid { get; set; }
        public Guid? RoleGuid { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid Guid { get; set; }

        public virtual TbMAccount? AccountGu { get; set; }
        public virtual TbMRole? RoleGu { get; set; }
    }
}
