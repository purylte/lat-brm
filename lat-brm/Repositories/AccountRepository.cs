using lat_brm.Contracts.Repositories;
using lat_brm.Data;
using lat_brm.Models;

namespace lat_brm.Repositories
{
    public class AccountRepository : GeneralRepository<TbMAccount>, IAccountRepository
    {
        public AccountRepository(EmployeeDbContext context) : base(context)
        {
        }
    }
}
