using lat_brm.Contracts;
using lat_brm.Dtos.University;
using lat_brm.Models;
using lat_brm.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace lat_brm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : ControllerBase
    {

        public readonly IUniversityRepository _universityRepository;

        public UniversityController(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<TbMUniversity> universities;
            try
            {
                universities = _universityRepository.GetAll();
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            var universityResponses = universities.Select(university => (UniversityResponse)university)
                .ToList();
            return Ok(universityResponses);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            TbMUniversity? university;
            try
            {
                university = _universityRepository.GetByGuid(id);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            if (university == null)
            {
                return NotFound("University not found");
            }

            return Ok((UniversityResponse)university);
        }

        [HttpPost]
        public IActionResult Insert(UniversityRequestInsert request)
        {
            TbMUniversity university;
            try
            {
                university = _universityRepository.Insert(request);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return Ok((UniversityResponse)university);
        }

        [HttpPut]
        public IActionResult Update(UniversityRequestUpdate request)
        {
            TbMUniversity? university;
            try
            {
                university = _universityRepository.GetByGuid(request.Guid);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            if (university == null)
            {
                return NotFound("University not found");
            }

            TbMUniversity requestObj = request;
            requestObj.CreatedDate = university.CreatedDate;

            TbMUniversity response;
            try
            {
                response = _universityRepository.Update(requestObj);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return Ok((UniversityResponse)response);
        }

        [HttpDelete("{id:Guid}")]
        public IActionResult Delete(Guid id)
        {
            if (_universityRepository.GetByGuid(id) == null)
            {
                return NotFound("University not found");
            }
            var university = _universityRepository.GetByGuid(id)!;
            try
            {
                _universityRepository.Delete(university);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }
    }
}
