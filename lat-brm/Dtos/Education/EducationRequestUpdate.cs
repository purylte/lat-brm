using lat_brm.Models;

namespace lat_brm.Dtos.Education
{
    public class EducationRequestUpdate
    {
        public Guid Guid { get; set; }
        public string? Major { get; set; }
        public string? Degree { get; set; }
        public double? Gpa { get; set; }
        public Guid UniversityGuid { get; set; }

        public static implicit operator TbMEducation(EducationRequestUpdate educationRequest)
        {
            return new TbMEducation
            {
                Guid = educationRequest.Guid,
                Major = educationRequest.Major,
                Degree = educationRequest.Degree,
                Gpa = educationRequest.Gpa,
                UniversityGuid = educationRequest.UniversityGuid,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
