using lat_brm.Dtos.University;

namespace lat_brm.Contracts.Services
{
    public interface IUniversityService
    {
        public List<UniversityResponse> GetAll();
        public UniversityResponse? GetById(Guid id);
        public UniversityResponse? Insert(UniversityRequestInsert request);
        public UniversityResponse? Update(UniversityRequestUpdate request);
        public bool Delete(Guid id);
    }
}
