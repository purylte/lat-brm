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
        private readonly EmployeeDbContext _employeeDbContext;

        public EmployeeService(IEmployeeRepository employeeRepository, EmployeeDbContext employeeDbContext)
        {
            _employeeRepository = employeeRepository;
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
    }
}
