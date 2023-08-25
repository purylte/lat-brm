using lat_brm.Contracts.Repositories;
using lat_brm.Data;
using lat_brm.Models;

namespace lat_brm.Repositories
{
    public class RoleRepository : GeneralRepository<TbMRole>, IRoleRepository
    {
        public RoleRepository(EmployeeDbContext context) : base(context)
        {
        }
    }
}
