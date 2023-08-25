using lat_brm.Contracts.Repositories;
using lat_brm.Contracts.Services;
using lat_brm.Dtos.AccountRole;
using lat_brm.Models;
using lat_brm.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace lat_brm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountRoleController : ControllerBase
    {
        private readonly IAccountRoleService _accountRoleService;

        public AccountRoleController(IAccountRoleService accountRoleService)
        {
            _accountRoleService = accountRoleService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var accountRoles = _accountRoleService.GetAll();
            return Ok(accountRoles);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            var accountRole = _accountRoleService.GetById(id);
            if (accountRole == null)
            {
                return NotFound("Account Role not found");
            }
            return Ok(accountRole);
        }

        [HttpPost]
        public IActionResult Insert(AccountRoleRequestInsert request)
        {
            var accountRole = _accountRoleService.Insert(request);
            if (accountRole == null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok(accountRole);
        }

        [HttpPut]
        public IActionResult Update(AccountRoleRequestUpdate request)
        {
            var accountRole = _accountRoleService.Update(request);
            if (accountRole == null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok(accountRole);
        }

        [HttpDelete("{id:Guid}")]
        public IActionResult Delete(Guid id)
        {
            var isDeleted = _accountRoleService.Delete(id);
            if (!isDeleted)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }
    }

}
