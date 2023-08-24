using System;
using System.Collections.Generic;

namespace lat_brm.Models
{
    public partial class TbMRoom
    {
        public string? Name { get; set; }
        public int? Floor { get; set; }
        public int? Capacity { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid Guid { get; set; }
    }
}
