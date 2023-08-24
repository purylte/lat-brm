using lat_brm.Contracts;
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
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IRoleRepository _roleRepository;

        public AccountRoleController(IAccountRoleRepository accountRoleRepository, IAccountRepository accountRepository, IRoleRepository roleRepository)
        {
            _accountRoleRepository = accountRoleRepository;
            _accountRepository = accountRepository;
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<TbMAccountRole> accountRoles;
            try
            {
                accountRoles = _accountRoleRepository.GetAll();
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            var result = accountRoles.Select(accountRole => (AccountRoleResponse)accountRole);

            return Ok(result);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            TbMAccountRole? accountRole;
            try
            {
                accountRole = _accountRoleRepository.GetByGuid(id);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            if (accountRole == null)
            {
                return NotFound("Account Role not found");
            }
            return Ok((AccountRoleResponse)accountRole);
        }

        [HttpPost]
        public IActionResult Insert(AccountRoleRequestInsert request)
        {
            if (_accountRepository.GetByGuid(request.AccountGuid) == null)
            {
                return NotFound("Account not found");
            }

            if (_roleRepository.GetByGuid(request.RoleGuid) == null)
            {
                return NotFound("Role not found");
            }
            TbMAccountRole accountRole;
            try
            {
                accountRole = _accountRoleRepository.Insert(request);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok((AccountRoleResponse)accountRole);
        }

        [HttpPut]
        public IActionResult Update(AccountRoleRequestUpdate request)
        {
            TbMAccount? account;
            TbMRole? role;
            TbMAccountRole? accountRole;

            try
            {
                accountRole = _accountRoleRepository.GetByGuid(request.Guid);
                account = _accountRepository.GetByGuid(request.AccountGuid);
                role = _roleRepository.GetByGuid(request.RoleGuid);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            if (accountRole == null)
            {
                return NotFound("Account Role not found");
            }
            if (account == null)
            {
                return NotFound("Account not found");
            }

            if (role == null)
            {
                return NotFound("Role not found");
            }


            TbMAccountRole requestObj = request;
            requestObj.CreatedDate = accountRole.CreatedDate;
            TbMAccountRole response;
            
            try
            {
                response = _accountRoleRepository.Update(requestObj);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok((AccountRoleResponse)response);
        }

        [HttpDelete("{id:Guid}")]
        public IActionResult Delete(Guid id)
        {
            var accountRole = _accountRoleRepository.GetByGuid(id);
            if (accountRole == null)
            {
                return NotFound("Account Role not found");
            }

            try
            {
                _accountRoleRepository.Delete(accountRole);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }
    }

}
