using lat_brm.Contracts;
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
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly EmployeeDbContext _employeeDbContext;

        public EmployeeController(IEmployeeRepository employeeRepository, IUniversityRepository universityRepository, IEducationRepository educationRepository, IAccountRepository accountRepository, EmployeeDbContext employeeDbContext)
        {
            _employeeRepository = employeeRepository;
            _universityRepository = universityRepository;
            _educationRepository = educationRepository;
            _accountRepository = accountRepository;
            _employeeDbContext = employeeDbContext;
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
            List<EmployeeResponseInfo> employeeResponses;
            try
            {
                // Left Join Employee and Education
                // Left Join Education and University
                // Populates response with null if theres no relation from Employee.
                employeeResponses = (
                from employee in _employeeDbContext.TbMEmployees
                join education in _employeeDbContext.TbMEducations on employee.Guid equals education.Guid into eduGroup
                from education in eduGroup.DefaultIfEmpty()
                join university in _employeeDbContext.TbMUniversities on education != null ? education.UniversityGuid : (Guid?)null equals university.Guid into uniGroup
                from university in uniGroup.DefaultIfEmpty()
                select new EmployeeResponseInfo
                {
                    EmployeeGuid = employee.Guid,
                    Nik = employee.Nik,
                    FullName = $"{employee.FirstName} {employee.LastName}",
                    PhoneNumber = employee.PhoneNumber,
                    Email = employee.Email,
                    Gender = employee.Gender,
                    BirthDate = employee.BirthDate,
                    HiringDate = employee.HiringDate,
                    Degree = education != null ? education.Degree : null,
                    Gpa = education != null ? education.Gpa : null,
                    Major = education != null ? education.Major : null,
                    UniversityCode = university != null ? university.Code : null,
                    UniversityName = university != null ? university.Name : null
                }).ToList();
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return Ok(employeeResponses);
        }


        [HttpGet("info/{id:Guid}")]
        public IActionResult GetInfoByGuid(Guid id)
        {
            IQueryable<EmployeeResponseInfo> employeeResponse;
            try
            {
                // Left Join Employee and Education
                // Left Join Education and University
                // Populates response with null if theres no relation from Employee.
                employeeResponse =
                from employee in _employeeDbContext.TbMEmployees
                where employee.Guid == id
                join education in _employeeDbContext.TbMEducations on employee.Guid equals education.Guid into eduGroup
                from education in eduGroup.DefaultIfEmpty()
                join university in _employeeDbContext.TbMUniversities on education != null ? education.UniversityGuid : (Guid?)null equals university.Guid into uniGroup
                from university in uniGroup.DefaultIfEmpty()
                select new EmployeeResponseInfo
                {
                    EmployeeGuid = employee.Guid,
                    Nik = employee.Nik,
                    FullName = $"{employee.FirstName} {employee.LastName}",
                    PhoneNumber = employee.PhoneNumber,
                    Email = employee.Email,
                    Gender = employee.Gender,
                    BirthDate = employee.BirthDate,
                    HiringDate = employee.HiringDate,
                    Degree = education != null ? education.Degree : null,
                    Gpa = education != null ? education.Gpa : null,
                    Major = education != null ? education.Major : null,
                    UniversityCode = university != null ? university.Code : null,
                    UniversityName = university != null ? university.Name : null
                };
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok(employeeResponse.SingleOrDefault());
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

            var passwordHash = BCryptPassword.HashPassword(request.Password);
            // Insert employee, education, and account
            using var transaction = _employeeDbContext.Database.BeginTransaction();
            try
            {
                var employee = _employeeRepository.Insert(new EmployeeRequestInsert
                {
                    Nik = request.Nik,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    BirthDate = request.BirthDate,
                    Gender = request.Gender,
                    HiringDate = request.HiringDate,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
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
                    Password = passwordHash
                });

                transaction.Commit();
            }
            catch (Exception e) when (e is DbUpdateException || e is DbUpdateConcurrencyException)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return Ok("Registration Successful!");
        }

        [HttpPost("login")]
        public IActionResult Login(EmployeeRequestLogin request)
        {
            TbMEmployee? employeeResponse;
            try
            {
                employeeResponse = (
                from employee in _employeeDbContext.TbMEmployees
                join account in _employeeDbContext.TbMAccounts on employee.Guid equals account.Guid
                where employee.Email == request.Email
                select employee
                ).Include(e => e.TbMAccount)
                .FirstOrDefault();
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            if (employeeResponse is null) {
                return NotFound("Login failed");
            }

            

            if (!BCryptPassword.ValidatePassword(request.Password, employeeResponse.TbMAccount.Password))
            {
                return NotFound("Login failed");
            }

            return Ok((EmployeeResponse)employeeResponse);
        }
    }
}
