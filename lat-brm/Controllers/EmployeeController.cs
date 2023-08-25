using lat_brm.Contracts.Repositories;
using lat_brm.Contracts.Services;
using lat_brm.Data;
using lat_brm.Dtos.Account;
using lat_brm.Dtos.Education;
using lat_brm.Dtos.Employee;
using lat_brm.Dtos.University;
using lat_brm.Models;
using lat_brm.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace lat_brm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var employees = _employeeService.GetAll();
            return Ok(employees);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            var employee = _employeeService.GetById(id);
            if (employee is null)
            {
                return NotFound("Employee not found");
            }
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult Insert(EmployeeRequestInsert request)
        {
            var employee = _employeeService.Insert(request);
            if (employee is null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok(employee);
        }

        [HttpPut]
        public IActionResult Update(EmployeeRequestUpdate request)
        {
            var employee = _employeeService.Update(request);
            if (employee is null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok(employee);
        }

        [HttpDelete("{id:Guid}")]
        public IActionResult Delete(Guid id)
        {
            var isDeleted = _employeeService.Delete(id);
            if (!isDeleted)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }

        [HttpGet("info")]
        public IActionResult GetInfo()
        {
            var employeeInfos = _employeeService.GetInfos();
            return Ok(employeeInfos);
        }


        [HttpGet("info/{id:Guid}")]
        public IActionResult GetInfoByGuid(Guid id)
        {
            var employeeInfo = _employeeService.GetInfoById(id);
            if (employeeInfo is null)
            {
                return NotFound("Employee not found");
            }
            return Ok(employeeInfo);
        }

        [HttpPost("register")]
        public IActionResult Register(EmployeeRequestRegister request)
        {
            var isRegistered = _employeeService.Register(request);
            if (!isRegistered)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok("Registration Successful");
        }

        [HttpPost("login")]
        public IActionResult Login(EmployeeRequestLogin request)
        {
            var isLogin = _employeeService.Login(request);
            if (!isLogin)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok("Login Successful");
        }
    }
}
