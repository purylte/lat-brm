using System;
using System.Collections.Generic;

namespace lat_brm.Models
{
    public partial class TbMEmployee
    {
        public string? Nik { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? Gender { get; set; }
        public DateTime? HiringDate { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid Guid { get; set; }

        public virtual TbMAccount? TbMAccount { get; set; }
        public virtual TbMEducation? TbMEducation { get; set; }
    }
}
