using lat_brm.Contracts.Repositories;
using lat_brm.Contracts.Services;
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

        private readonly IUniversityService _universityService;

        public UniversityController(IUniversityService universityService)
        {
            _universityService = universityService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var universityResponses = _universityService.GetAll();
            return Ok(universityResponses);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            var university = _universityService.GetById(id);
            if (university == null)
            {
                return NotFound("University not found");
            }
            return Ok(university);
        }

        [HttpPost]
        public IActionResult Insert(UniversityRequestInsert request)
        {
            var university = _universityService.Insert(request);
            if (university == null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return Ok(university);
        }

        [HttpPut]
        public IActionResult Update(UniversityRequestUpdate request)
        {
            var university = _universityService.Update(request);
            if (university == null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok(university);
        }

        [HttpDelete("{id:Guid}")]
        public IActionResult Delete(Guid id)
        {
            var isDeleted = _universityService.Delete(id);
            if (!isDeleted)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }
    }
}
