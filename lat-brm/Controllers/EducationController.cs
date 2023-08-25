using lat_brm.Contracts.Repositories;
using lat_brm.Contracts.Services;
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

        private readonly IEducationService _educationService;

        public EducationController(IEducationService educationService)
        {
            _educationService = educationService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var educations = _educationService.GetAll();
            return Ok(educations);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            var education = _educationService.GetById(id);
            if (education is null)
            {
                return NotFound("Education not found");
            }
            return Ok(education);
        }

        [HttpPost]
        public IActionResult Insert(EducationRequestInsert request)
        {
            var education = _educationService.Insert(request);
            if (education is null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok(education);
        }

        [HttpPut]
        public IActionResult Update(EducationRequestUpdate request)
        {
            var education = _educationService.Update(request);
            if (education is null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok(education);
        }

        [HttpDelete("{id:Guid}")]
        public IActionResult Delete(Guid id)
        {
            var isDeleted = _educationService.Delete(id);
            if (!isDeleted)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }
    }
}
