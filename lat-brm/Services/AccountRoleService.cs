using lat_brm.Contracts.Repositories;
using lat_brm.Contracts.Services;
using lat_brm.Dtos.AccountRole;
using lat_brm.Models;
using Microsoft.AspNetCore.Mvc;

namespace lat_brm.Services
{
    public class AccountRoleService : IAccountRoleService
    {
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IRoleRepository _roleRepository;

        public AccountRoleService(IAccountRoleRepository accountRoleRepository, IAccountRepository accountRepository, IRoleRepository roleRepository)
        {
            _accountRoleRepository = accountRoleRepository;
            _accountRepository = accountRepository;
            _roleRepository = roleRepository;
        }

        public bool Delete(Guid id)
        {
            var accountRole = _accountRoleRepository.GetByGuid(id);
            if (accountRole == null)
            {
                return false;
            }
            try
            {
                _accountRoleRepository.Delete(accountRole);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<AccountRoleResponse> GetAll()
        {
            var accountRoles = _accountRoleRepository.GetAll();
            var result = accountRoles.Select(accountRole => (AccountRoleResponse)accountRole);
            return result;
        }

        public AccountRoleResponse? GetById(Guid id)
        {
            var accountRole = _accountRoleRepository.GetByGuid(id);
            if (accountRole == null)
            {
                return null;
            }
            return (AccountRoleResponse)accountRole;
        }

        public AccountRoleResponse? Insert(AccountRoleRequestInsert request)
        {
            if (_accountRepository.GetByGuid(request.AccountGuid) is null)
            {
                return null;
            }

            if (_roleRepository.GetByGuid(request.RoleGuid) is null)
            {
                return null;
            }

            TbMAccountRole accountRole;
            try
            {
                accountRole = _accountRoleRepository.Insert(request);
            }
            catch (Exception)
            {
                return null;
            }
            return (AccountRoleResponse)accountRole;
        }

        public AccountRoleResponse? Update(AccountRoleRequestUpdate request)
        {
            var accountRole = _accountRoleRepository.GetByGuid(request.Guid);
            if (accountRole is null 
                || _accountRepository.GetByGuid(request.AccountGuid) is null 
                || _roleRepository.GetByGuid(request.RoleGuid) is null)
            {
                return null;
            }

            TbMAccountRole requestObj = request;
            requestObj.CreatedDate = accountRole.CreatedDate;
            TbMAccountRole updatedAccountRole;

            try
            {
                updatedAccountRole = _accountRoleRepository.Update(requestObj);
            }
            catch
            {
                return null;
            }
            return (AccountRoleResponse)updatedAccountRole;
        }
    }
}
