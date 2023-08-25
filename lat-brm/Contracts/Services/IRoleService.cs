using lat_brm.Dtos.Role;

namespace lat_brm.Contracts.Services
{
    public interface IRoleService
    {
        IEnumerable<RoleResponse> GetAll();
        RoleResponse? GetById(Guid id);
        RoleResponse? Insert(RoleRequestInsert request);
        RoleResponse? Update(RoleRequestUpdate request);
        bool Delete(Guid id);
    }
}
