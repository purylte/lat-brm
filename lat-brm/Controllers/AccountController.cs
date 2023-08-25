using lat_brm.Contracts.Repositories;
using lat_brm.Dtos.Account;
using lat_brm.Models;
using lat_brm.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace lat_brm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public AccountController(IAccountRepository accountRepository, IEmployeeRepository employeeRepository)
        {
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<TbMAccount> accounts;
            try
            {
                accounts = _accountRepository.GetAll();
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            var accountResponses = accounts.Select(account => (AccountResponse)account);
            return Ok(accountResponses);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            TbMAccount? account;
            try
            {
                account = _accountRepository.GetByGuid(id);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            if (account == null)
            {
                return NotFound("Account not found");
            }
            return Ok((AccountResponse)account);
        }

        [HttpPost]
        public IActionResult Insert(AccountRequestInsert request)
        {
            TbMEmployee? employee;
            try
            {
                employee = _employeeRepository.GetByGuid(request.Guid);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            if (employee == null)
            {
                return NotFound("Employee not found");
            }
            TbMAccount account;
            try
            {
                account = _accountRepository.Insert(request);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            account.CreatedDate = employee.CreatedDate;
            return Ok((AccountResponse)account);
        }

        [HttpPut]
        public IActionResult Update(AccountRequestUpdate request)
        {
            var guid = request.Guid;
            TbMAccount? account;
            TbMEmployee? employee;
            try
            {
                account = _accountRepository.GetByGuid(guid);
                employee = _employeeRepository.GetByGuid(guid);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            if (account == null)
            {
                return NotFound("Account not found");
            }

            if (employee == null)
            {
                return NotFound("Employee not found");
            }

            TbMAccount requestObj = request;
            requestObj.CreatedDate = account.CreatedDate;

            TbMAccount response;
            try
            {
                response = _accountRepository.Update(requestObj);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok((AccountResponse)response);
        }

        [HttpDelete("{id:Guid}")]
        public IActionResult Delete(Guid id)
        {
            TbMAccount? account;
            try
            {
                account = _accountRepository.GetByGuid(id);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            if (account == null)
            {
                return NotFound("Account not found");
            }

            try
            {
                _accountRepository.Delete(account);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }
    }

}
