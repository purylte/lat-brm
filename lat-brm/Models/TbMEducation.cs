using System;
using System.Collections.Generic;

namespace lat_brm.Models
{
    public partial class TbMEducation
    {
        public string? Major { get; set; }
        public string? Degree { get; set; }
        public double? Gpa { get; set; }
        public Guid? UniversityGuid { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid Guid { get; set; }

        public virtual TbMEmployee? Gu { get; set; } = null!;
        public virtual TbMUniversity? UniversityGu { get; set; }
    }
}
