using lat_brm.Contracts;
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
        private readonly IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<TbMRole> roles;
            try
            {
                roles = _roleRepository.GetAll();
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            var roleReponses = roles.Select(role => (RoleResponse)role);
            return Ok(roleReponses);

        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            TbMRole? role;
            try
            {
                role = _roleRepository.GetByGuid(id);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            if (role == null)
            {
                return NotFound("Role not found");
            }
            return Ok((RoleResponse)role);
        }

        [HttpPost]
        public IActionResult Insert(RoleRequestInsert request)
        {
            TbMRole role;
            try
            {
                role = _roleRepository.Insert(request);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return Ok((RoleResponse)role);
        }

        [HttpPut]
        public IActionResult Update(RoleRequestUpdate request)
        {
            TbMRole? role;
            try
            {
                role = _roleRepository.GetByGuid(request.Guid);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            
            if (role == null)
            {
                return NotFound("Role not found");
            }

            TbMRole requestObj = request;
            requestObj.CreatedDate = role.CreatedDate;

            TbMRole response;
            try
            {
                response = _roleRepository.Update(requestObj);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return Ok(role);
        }

        [HttpDelete("{id:Guid}")]
        public IActionResult Delete(Guid id)
        {
            TbMRole? role;
            try
            {
                role = _roleRepository.GetByGuid(id);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            if (role == null)
            {
                return NotFound("Role not found");
            }
            try
            {
                _roleRepository.Delete(role);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }
    }
}
