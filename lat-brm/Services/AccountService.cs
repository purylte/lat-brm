using lat_brm.Contracts.Repositories;
using lat_brm.Contracts.Services;
using lat_brm.Data;
using lat_brm.Dtos.Account;
using lat_brm.Dtos.Education;
using lat_brm.Dtos.Employee;
using lat_brm.Dtos.University;
using lat_brm.Models;
using lat_brm.Repositories;
using lat_brm.Utilities;
using Microsoft.EntityFrameworkCore;

namespace lat_brm.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly EmployeeDbContext _employeeDbContext;

        public AccountService(IAccountRepository accountRepository, IEmployeeRepository employeeRepository, IUniversityRepository universityRepository, IEducationRepository educationRepository, EmployeeDbContext employeeDbContext)
        {
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
            _universityRepository = universityRepository;
            _educationRepository = educationRepository;
            _employeeDbContext = employeeDbContext;
        }

        public bool Delete(Guid id)
        {
            var account = _accountRepository.GetByGuid(id);
            if (account is null) return false;
            try
            {
                _accountRepository.Delete(account);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<AccountResponse> GetAll()
        {
            var accounts = _accountRepository.GetAll();
            return accounts.Select(account => (AccountResponse)account);
        }

        public AccountResponse? GetById(Guid id)
        {
            var account = _accountRepository.GetByGuid(id);
            if (account is null) return null;
            return (AccountResponse)account;
        }

        public AccountResponse? Insert(AccountRequestInsert request)
        {
            var employee = _employeeRepository.GetByGuid(request.Guid);
            if (employee is null) return null;

            TbMAccount account;
            try
            {
                account = _accountRepository.Insert(request);
            }
            catch (Exception)
            {
                return null;
            }
            return (AccountResponse)account;

        }

        public bool Register(AccountRequestRegister request)
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

        public bool Login(AccountRequestLogin request)
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


        public AccountResponse? Update(AccountRequestUpdate request)
        {
            var account = _accountRepository.GetByGuid(request.Guid);
            if (account is null) return null;

            var employee = _employeeRepository.GetByGuid(request.Guid);
            if (employee is null) return null;

            TbMAccount requestObj = request;
            requestObj.CreatedDate = account.CreatedDate;

            TbMAccount updatedAccount;
            try
            {
                updatedAccount = _accountRepository.Update(requestObj);
            }
            catch (Exception)
            {
                return null;
            }
            return (AccountResponse)updatedAccount;
        }
    }
}
