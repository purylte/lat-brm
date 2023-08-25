using lat_brm.Contracts.Repositories;
using lat_brm.Contracts.Services;
using lat_brm.Dtos.Role;
using lat_brm.Models;

namespace lat_brm.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public bool Delete(Guid id)
        {
            var role = _roleRepository.GetByGuid(id);
            if (role is null) return false;
            try
            {
                _roleRepository.Delete(role);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<RoleResponse> GetAll()
        {
            var roles = _roleRepository.GetAll();
            return roles.Select(role => (RoleResponse)role);
        }

        public RoleResponse? GetById(Guid id)
        {
            var role = _roleRepository.GetByGuid(id);
            if (role is null) return null;
            return (RoleResponse)role;
        }

        public RoleResponse? Insert(RoleRequestInsert request)
        {
            TbMRole role;
            try
            {
                role = _roleRepository.Insert(request);
            }
            catch (Exception)
            {
                return null;
            }
            return (RoleResponse)role;
        }

        public RoleResponse? Update(RoleRequestUpdate request)
        {
            var role = _roleRepository.GetByGuid(request.Guid);
            if (role is null) return null;

            TbMRole requestObj = request;
            requestObj.CreatedDate = role.CreatedDate;

            TbMRole updatedRole;
            try
            {
                updatedRole = _roleRepository.Update(requestObj);
            }
            catch (Exception)
            {
                return null;
            }
            return (RoleResponse)updatedRole;
        }
    }
}
