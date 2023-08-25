using lat_brm.Contracts.Repositories;
using lat_brm.Data;
using lat_brm.Models;

namespace lat_brm.Repositories
{
    public class AccountRoleRepository : GeneralRepository<TbMAccountRole>, IAccountRoleRepository
    {
        public AccountRoleRepository(EmployeeDbContext context) : base(context)
        {
        }
    }
}
