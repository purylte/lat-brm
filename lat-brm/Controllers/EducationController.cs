using lat_brm.Contracts;
using lat_brm.Dtos.Education;
using lat_brm.Models;
using lat_brm.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace lat_brm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationController : ControllerBase
    {

        private readonly IEducationRepository _educationRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUniversityRepository _universityRepository;

        public EducationController(IEducationRepository educationRepository, IEmployeeRepository employeeRepository, IUniversityRepository universityRepository)
        {
            _educationRepository = educationRepository;
            _employeeRepository = employeeRepository;
            _universityRepository = universityRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<TbMEducation> educations;
            try
            {
                educations = _educationRepository.GetAll();
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            var educationResponses = educations.Select(education => (EducationResponse)education);

            return Ok(educationResponses);

        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            TbMEducation? education;
            try
            {
                education = _educationRepository.GetByGuid(id);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            if (education == null)
            {
                return NotFound("Education not found");
            }
            return Ok((EducationResponse)education);
        }

        [HttpPost]
        public IActionResult Insert(EducationRequestInsert request)
        {
            TbMEmployee? employee;
            TbMUniversity? university;

            try
            {
                employee = _employeeRepository.GetByGuid(request.Guid);
                university = _universityRepository.GetByGuid(request.UniversityGuid);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            if (employee == null)
            {
                return NotFound("Employee not found");
            }

            if (university == null)
            {
                return NotFound("University not found");
            }

            TbMEducation education;
            try
            {
                education = _educationRepository.Insert(request);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return Ok((EducationResponse)education);
        }

        [HttpPut]
        public IActionResult Update(EducationRequestUpdate request)
        {
            TbMEducation? education;
            try
            {
                education = _educationRepository.GetByGuid(request.Guid);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            if (education == null)
            {
                return NotFound("Education not found");
            }

            TbMEducation requestObj = request;
            requestObj.CreatedDate = education.CreatedDate;

            TbMEducation response;
            try
            {
                response = _educationRepository.Update(requestObj);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok((EducationResponse)response);
        }

        [HttpDelete("{id:Guid}")]
        public IActionResult Delete(Guid id)
        {
            TbMEducation? education;

            try
            {
                education = _educationRepository.GetByGuid(id);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            if (education == null)
            {
                return NotFound("Education not found");
            }

            try
            {
                _educationRepository.Delete(education);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }
    }
}
