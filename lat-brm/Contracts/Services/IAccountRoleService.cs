using lat_brm.Dtos.AccountRole;

namespace lat_brm.Contracts.Services
{
    public interface IAccountRoleService
    {
        IEnumerable<AccountRoleResponse> GetAll();
        AccountRoleResponse? GetById(Guid id);
        AccountRoleResponse? Insert(AccountRoleRequestInsert request);
        AccountRoleResponse? Update(AccountRoleRequestUpdate request);
        bool Delete(Guid id);
    }
}
