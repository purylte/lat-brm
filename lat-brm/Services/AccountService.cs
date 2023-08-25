using lat_brm.Contracts.Repositories;
using lat_brm.Contracts.Services;
using lat_brm.Dtos.Account;
using lat_brm.Models;

namespace lat_brm.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public AccountService(IAccountRepository accountRepository, IEmployeeRepository employeeRepository)
        {
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
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
