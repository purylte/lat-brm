using lat_brm.Contracts.Repositories;
using lat_brm.Contracts.Services;
using lat_brm.Dtos.Education;
using lat_brm.Models;
using Microsoft.AspNetCore.Mvc;

namespace lat_brm.Services
{
    public class EducationService : IEducationService
    {
        private readonly IEducationRepository _educationRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUniversityRepository _universityRepository;

        public EducationService(IEducationRepository educationRepository, IEmployeeRepository employeeRepository, IUniversityRepository universityRepository)
        {
            _educationRepository = educationRepository;
            _employeeRepository = employeeRepository;
            _universityRepository = universityRepository;
        }

        public bool Delete(Guid id)
        {
            var education = _educationRepository.GetByGuid(id);
            if (education is null) return false;
            try
            {
                _educationRepository.Delete(education);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<EducationResponse> GetAll()
        {
            var educations = _educationRepository.GetAll();
            return educations.Select(education => (EducationResponse)education);
        }

        public EducationResponse? GetById(Guid id)
        {
            var education = _educationRepository.GetByGuid(id);
            if (education is null) return null;
            return (EducationResponse)education;
        }

        public EducationResponse? Insert(EducationRequestInsert request)
        {
            var employee = _employeeRepository.GetByGuid(request.Guid);
            if (employee is null) return null;

            var university = _universityRepository.GetByGuid(request.UniversityGuid);
            if (university is null) return null;

            TbMEducation education;
            try
            {
                education = _educationRepository.Insert(request);
            }
            catch (Exception)
            {
                return null;
            }
            return (EducationResponse)education;
        }

        public EducationResponse? Update(EducationRequestUpdate request)
        {
            var education = _educationRepository.GetByGuid(request.Guid);
            if (education is null) return null;

            TbMEducation requestObj = request;
            requestObj.CreatedDate = education.CreatedDate;

            TbMEducation updatedEducation;
            try
            {
                updatedEducation = _educationRepository.Update(requestObj);
            }
            catch
            {
                return null;
            }
            return (EducationResponse)updatedEducation;
        }
    }
}
