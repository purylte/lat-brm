using System;
using System.Collections.Generic;

namespace lat_brm.Models
{
    public partial class TbMUniversity
    {
        public TbMUniversity()
        {
            TbMEducations = new HashSet<TbMEducation>();
        }

        public string? Code { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid Guid { get; set; }

        public virtual ICollection<TbMEducation>? TbMEducations { get; set; }
    }
}
