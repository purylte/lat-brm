using lat_brm.Dtos.Education;

namespace lat_brm.Contracts.Services
{
    public interface IEducationService
    {
        IEnumerable<EducationResponse> GetAll();
        EducationResponse? GetById(Guid id);
        EducationResponse? Insert(EducationRequestInsert request);
        EducationResponse? Update(EducationRequestUpdate request);
        bool Delete(Guid id);
    }
}
