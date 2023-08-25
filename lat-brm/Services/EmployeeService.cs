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

namespace lat_brm.Services
{

    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly EmployeeDbContext _employeeDbContext;

        public EmployeeService(IEmployeeRepository employeeRepository, IUniversityRepository universityRepository, IEducationRepository educationRepository, IAccountRepository accountRepository, EmployeeDbContext employeeDbContext)
        {
            _employeeRepository = employeeRepository;
            _universityRepository = universityRepository;
            _educationRepository = educationRepository;
            _accountRepository = accountRepository;
            _employeeDbContext = employeeDbContext;
        }

        public bool Delete(Guid id)
        {
            var employee = _employeeRepository.GetByGuid(id);
            if (employee is null) return false;
            try
            {
                _employeeRepository.Delete(employee);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<EmployeeResponse> GetAll()
        {
            var employees = _employeeRepository.GetAll();
            return employees.Select(employee => (EmployeeResponse)employee);
        }

        public EmployeeResponse? GetById(Guid id)
        {
            var employee = _employeeRepository.GetByGuid(id);
            if (employee is null) return null;
            return (EmployeeResponse)employee;
        }

        public EmployeeResponse? Insert(EmployeeRequestInsert request)
        {
            TbMEmployee employee;
            try
            {
                employee = _employeeRepository.Insert(request);
            }
            catch (Exception)
            {
                return null;
            }
            return (EmployeeResponse)employee;
        }

        public EmployeeResponse? Update(EmployeeRequestUpdate request)
        {
            var employee = _employeeRepository.GetByGuid(request.Guid);
            if (employee is null) return null;

            TbMEmployee requestObj = request;
            requestObj.CreatedDate = employee.CreatedDate;

            TbMEmployee updatedEmployee;
            try
            {
                updatedEmployee = _employeeRepository.Update(requestObj);
            }
            catch (Exception)
            {
                return null;
            }
            return (EmployeeResponse)updatedEmployee;
        }

        public IEnumerable<EmployeeResponseInfo>? GetInfos()
        {
            IEnumerable<EmployeeResponseInfo> employeeResponses;
            try
            {
                // Left Join Employee and Education
                // Left Join Education and University
                // Populates response with null if theres no relation from Employee.
                employeeResponses = 
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
                };
            }
            catch (Exception)
            {
                return null;
            }

            return employeeResponses;
        }

        public EmployeeResponseInfo? GetInfoById(Guid id)
        {
            IQueryable<EmployeeResponseInfo> employeeResponses;
            try
            {
                // Left Join Employee and Education
                // Left Join Education and University
                // Populates response with null if theres no relation from Employee.
                employeeResponses =
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
                return null;
            }
            var employeeResponse = employeeResponses.SingleOrDefault();
            return employeeResponse;
        }

        public bool Register(EmployeeRequestRegister request)
        {
            if (request.Password != request.ConfirmPassword)
            {
                return false;
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
                return false;
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
                return false;
            }

            return true;
        }

        public bool Login(EmployeeRequestLogin request)
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
                return false;
            }

            if (employeeResponse is null) return false;

            var employeeAccount = employeeResponse.TbMAccount;
            if (employeeAccount is null || employeeAccount.Password is null) return false;

            if (!BCryptPassword.ValidatePassword(request.Password, employeeAccount.Password))
            {
                return false;
            }

            return true;
        }
    }


}
