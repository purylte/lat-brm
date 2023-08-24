using lat_brm.Contracts;
using lat_brm.Dtos.Account;
using lat_brm.Dtos.Education;
using lat_brm.Dtos.Employee;
using lat_brm.Dtos.University;
using lat_brm.Models;
using lat_brm.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lat_brm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IAccountRepository _accountRepository;

        public EmployeeController(IEmployeeRepository employeeRepository, IUniversityRepository universityRepository, IEducationRepository educationRepository, IAccountRepository accountRepository)
        {
            _employeeRepository = employeeRepository;
            _universityRepository = universityRepository;
            _educationRepository = educationRepository;
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<TbMEmployee> employees;
            try
            {
                employees = _employeeRepository.GetAll();
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            var employeeResponses = employees.Select(employee => (EmployeeResponse)employee);
            return Ok(employeeResponses);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            TbMEmployee? employee;
            try
            {
                employee = _employeeRepository.GetByGuid(id);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            if (employee == null)
            {
                return NotFound("Employee not found");
            }
            return Ok((EmployeeResponse)employee);
        }

        [HttpPost]
        public IActionResult Insert(EmployeeRequestInsert request)
        {
            TbMEmployee employee;
            try
            {
                employee = _employeeRepository.Insert(request);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok((EmployeeResponse)employee);
        }

        [HttpPut]
        public IActionResult Update(EmployeeRequestUpdate request)
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

            TbMEmployee requestObj = request;
            requestObj.CreatedDate = employee.CreatedDate;

            TbMEmployee response;
            try
            {
                response = _employeeRepository.Update(requestObj);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return Ok((EmployeeResponse)response);
        }

        [HttpDelete("{id:Guid}")]
        public IActionResult Delete(Guid id)
        {
            TbMEmployee? employee;
            try
            {
                employee = _employeeRepository.GetByGuid(id);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            if (employee == null)
            {
                return NotFound("Employee not found");
            }

            try
            {
                _employeeRepository.Delete(employee);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        [HttpGet("info")]
        public IActionResult GetInfo()
        {
            var employees = _employeeRepository.GetAll();
            var employeeResponse = new List<EmployeeResponseInfo>();
            foreach (var employee in employees)
            {
                var education = _educationRepository.GetByGuid(employee.Guid);
                if (education is null) continue;
                if (education.UniversityGuid is null) continue;
                var university = _universityRepository.GetByGuid((Guid)education.UniversityGuid);
                if (university is null) continue;

                employeeResponse.Add(new EmployeeResponseInfo
                {
                    EmployeeGuid = employee.Guid,
                    Nik = employee.Nik,
                    FullName = $"{employee.FirstName} {employee.LastName}",
                    PhoneNumber = employee.PhoneNumber,
                    Email = employee.Email,
                    Gender = employee.Gender,
                    BirthDate = employee.BirthDate,
                    HiringDate = employee.HiringDate,
                    Degree = education.Degree,
                    Gpa = education.Gpa,
                    Major = education.Major,
                    UniversityCode = university.Code,
                    UniversityName = university.Name
                });
            }
            return Ok(employeeResponse);
        }


        [HttpGet("info/{id:Guid}")]
        public IActionResult GetInfoByGuid(Guid id)
        {
            var employee = _employeeRepository.GetByGuid(id);
            if (employee == null)
            {
                return NotFound($"Employee with guid {id} not found");
            }
            var education = _educationRepository.GetByGuid(employee.Guid);
            if (education is null)
            {
                return NotFound($"Education with guid {employee.Guid} not found");
            }

            if (education.UniversityGuid is null)
            {
                return NotFound("University not found");
            }
            var university = _universityRepository.GetByGuid((Guid)education.UniversityGuid);
            if (university is null)
            {
                return NotFound($"University with guid {education.UniversityGuid}");
            }

            return Ok(new EmployeeResponseInfo
            {
                EmployeeGuid = employee.Guid,
                Nik = employee.Nik,
                FullName = $"{employee.FirstName} {employee.LastName}",
                PhoneNumber = employee.PhoneNumber,
                Email = employee.Email,
                Gender = employee.Gender,
                BirthDate = employee.BirthDate,
                HiringDate = employee.HiringDate,
                Degree = education.Degree,
                Gpa = education.Gpa,
                Major = education.Major,
                UniversityCode = university.Code,
                UniversityName = university.Name
            });
        }

        [HttpPost("register")]
        public IActionResult Register(EmployeeRequestRegister request)
        {
            if (request.Password != request.ConfirmPassword)
            {
                return BadRequest("Password and confirm password must be same");
            }
            // Check if university exist then get university object else make new
            var university = _universityRepository.GetByCodeAndName(request.UniversityCode, request.UniversityName);

            try
            {
                university ??= _universityRepository.Insert(new UniversityRequestInsert
                {
                    Code = request.UniversityCode,
                    Name = request.UniversityName
                });
            }
            catch (Exception e) when (e is DbUpdateException || e is DbUpdateConcurrencyException)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            try
            {
                // Insert employee, education, and account
                var employee = _employeeRepository.Insert(new EmployeeRequestInsert
                {
                    Nik = request.Nik,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    BirthDate = request.BirthDate,
                    Gender = request.Gender,
                    HiringDate = request.HiringDate,
                });

                _educationRepository.Insert(new EducationRequestInsert
                {
                    Guid = employee.Guid,
                    Major = request.Major,
                    Degree = request.Degree,
                    Gpa = request.Gpa,
                    UniversityGuid = university.Guid,
                });

                _accountRepository.Insert(new AccountRequestInsert
                {
                    Guid = employee.Guid,
                    ExpiredTime = DateTime.Now.AddYears(1),
                    IsDeleted = 0,
                    IsUsed = 1,
                    Otp = 12345,
                    Password = request.Password,
                });
            }
            catch (Exception e) when (e is DbUpdateException || e is DbUpdateConcurrencyException)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return Ok("Registration Successful!");
        }
    }
}
