using lat_brm.Contracts.Repositories;
using lat_brm.Contracts.Services;
using lat_brm.Dtos.Role;
using lat_brm.Models;
using lat_brm.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace lat_brm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var roles = _roleService.GetAll();
            return Ok(roles);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            var role = _roleService.GetById(id);
            if (role is null) return NotFound("Role not found");
            return Ok(role);
        }

        [HttpPost]
        public IActionResult Insert(RoleRequestInsert request)
        {
            var role = _roleService.Insert(request);
            if (role is null)
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            return Ok(role);
        }

        [HttpPut]
        public IActionResult Update(RoleRequestUpdate request)
        {
            var role = _roleService.Update(request);
            if (role is null)
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            return Ok(role);
        }

        [HttpDelete("{id:Guid}")]
        public IActionResult Delete(Guid id)
        {
            var isDeleted = _roleService.Delete(id);
            if (!isDeleted) return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            return NoContent();
        }
    }
}
