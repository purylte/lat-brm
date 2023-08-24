using lat_brm.Models;

namespace lat_brm.Dtos.University
{
    public class UniversityRequestUpdate
    {
        public Guid Guid { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;


        public static implicit operator TbMUniversity(UniversityRequestUpdate universityRequest)
        {
            return new TbMUniversity
            {
                Guid = universityRequest.Guid,
                Name = universityRequest.Name,
                Code = universityRequest.Code,
                ModifiedDate = DateTime.Now,
            };

        }
    }
}
