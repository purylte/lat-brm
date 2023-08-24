using lat_brm.Models;

namespace lat_brm.Dtos.University
{
    public class UniversityRequestInsert
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;

        public static implicit operator TbMUniversity(UniversityRequestInsert universityRequest)
        {
            return new TbMUniversity
            {
                Guid = Guid.NewGuid(),
                Name = universityRequest.Name,
                Code = universityRequest.Code,
                ModifiedDate = DateTime.Now,
                CreatedDate = DateTime.Now

            };
        }
    }
}
