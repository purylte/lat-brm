using System;
using System.Collections.Generic;

namespace lat_brm.Models
{
    public partial class TbMAccount
    {
        public TbMAccount()
        {
            TbMAccountRoles = new HashSet<TbMAccountRole>();
        }

        public string? Password { get; set; }
        public ulong? IsDeleted { get; set; }
        public int? Otp { get; set; }
        public ulong? IsUsed { get; set; }
        public DateTime? ExpiredTime { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid Guid { get; set; }
        public virtual TbMEmployee? Gu { get; set; } = null!;
        public virtual ICollection<TbMAccountRole>? TbMAccountRoles { get; set; }
    }
}
