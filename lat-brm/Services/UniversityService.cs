using lat_brm.Contracts.Repositories;
using lat_brm.Contracts.Services;
using lat_brm.Dtos.University;
using lat_brm.Models;
using Microsoft.EntityFrameworkCore;

namespace lat_brm.Services
{
    public class UniversityService : IUniversityService
    {
        public readonly IUniversityRepository _universityRepository;

        public UniversityService(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }

        public IEnumerable<UniversityResponse> GetAll()
        {
            var universityResponses = _universityRepository.GetAll()
                .Select(university => (UniversityResponse)university);

            return universityResponses;
        }

        public UniversityResponse? GetById(Guid id)
        {
            var university = _universityRepository.GetByGuid(id);
            return university is null ? null : (UniversityResponse)university;
        }

        public UniversityResponse? Insert(UniversityRequestInsert request)
        {
            TbMUniversity universityAdded;
            try
            {
                universityAdded = _universityRepository.Insert(request);
            }
            catch (Exception e) when (e is DbUpdateException or DbUpdateConcurrencyException)
            {
                return null;
            }
            return (UniversityResponse)universityAdded;
        }

        public UniversityResponse? Update(UniversityRequestUpdate request)
        {
            var university = _universityRepository.GetByGuid(request.Guid);
            if (university is null) return null;
            
            TbMUniversity requestObj = request;
            requestObj.CreatedDate = university.CreatedDate;
            TbMUniversity universityUpdated;

            try
            {
                universityUpdated = _universityRepository.Update(requestObj);
            }
            catch (Exception e) when (e is DbUpdateException or DbUpdateConcurrencyException)
            {
                return null;
            }
            return (UniversityResponse)universityUpdated;
        }

        public bool Delete(Guid id)
        {
            var university = _universityRepository.GetByGuid(id);
            if (university is null) return false;
           
            try
            {
                _universityRepository.Delete(university);
            }
            catch (Exception e) when (e is DbUpdateException or DbUpdateConcurrencyException)
            {
                return false;
            }
            return true;
        }
    }
}
