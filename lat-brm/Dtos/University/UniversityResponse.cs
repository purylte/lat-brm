using lat_brm.Models;

namespace lat_brm.Dtos.University
{
    public class UniversityResponse
    {
        public Guid Guid { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? CreatedDate { get; set; }

        public static explicit operator UniversityResponse(TbMUniversity university)
        {
            return new UniversityResponse
            {
                Guid = university.Guid,
                Name = university.Name,
                Code = university.Code,
                ModifiedDate = university.ModifiedDate,
                CreatedDate = university.CreatedDate
            };
        }
    }

}
