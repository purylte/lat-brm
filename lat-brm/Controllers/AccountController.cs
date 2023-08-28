using lat_brm.Contracts.Authentications;
using lat_brm.Contracts.Repositories;
using lat_brm.Contracts.Services;
using lat_brm.Dtos.Account;
using lat_brm.Models;
using lat_brm.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lat_brm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IJwtAuthentication _jwtAuthentication;
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService, IJwtAuthentication jwtAuthentication)
        {
            _accountService = accountService;
            _jwtAuthentication = jwtAuthentication;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var accounts = _accountService.GetAll();
            return Ok(accounts);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            var account = _accountService.GetById(id);
            if (account == null)
            {
                return NotFound("Account not found");
            }
            return Ok(account);
        }

        [HttpPost]
        public IActionResult Insert(AccountRequestInsert request)
        {
            var account = _accountService.Insert(request);
            if (account == null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok(account);
        }

        [HttpPut]
        public IActionResult Update(AccountRequestUpdate request)
        {
            var account = _accountService.Update(request);
            if (account == null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok(account);
        }

        [HttpDelete("{id:Guid}")]
        public IActionResult Delete(Guid id)
        {
            var isDeleted = _accountService.Delete(id);
            if (!isDeleted)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }

        [HttpPost("register")]
        public IActionResult Register(AccountRequestRegister request)
        {
            var tokenResponse = _accountService.Register(request);
            if (tokenResponse == null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok(tokenResponse);
        }

        [HttpPost("login")]
        public IActionResult Login(AccountRequestLogin request)
        {
            var tokenResponse = _accountService.Login(request);
            if (tokenResponse == null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok(tokenResponse);
        }

        [HttpGet("authorized"), Authorize]
        public async Task<IActionResult> GetAsync()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            return Ok($"Hello {_jwtAuthentication.GetEmail(token!)}");
        }
    }

}
