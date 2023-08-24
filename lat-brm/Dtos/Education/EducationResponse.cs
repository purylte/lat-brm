using lat_brm.Models;

namespace lat_brm.Dtos.Education
{
    public class EducationResponse
    {
        public string? Major { get; set; }
        public string? Degree { get; set; }
        public double? Gpa { get; set; }
        public Guid? UniversityGuid { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid Guid { get; set; }

        public static explicit operator EducationResponse(TbMEducation education)
        {
            return new EducationResponse
            {
                Guid = education.Guid,
                Major = education.Major,
                Degree = education.Degree,
                Gpa = education.Gpa,
                UniversityGuid = education.UniversityGuid,
                CreatedDate = education.CreatedDate,
                ModifiedDate = education.ModifiedDate,
            };
        }
    }
}
