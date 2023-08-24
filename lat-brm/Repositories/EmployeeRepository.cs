using lat_brm.Contracts;
using lat_brm.Data;
using lat_brm.Models;

namespace lat_brm.Repositories
{
    public class EmployeeRepository : GeneralRepository<TbMEmployee>, IEmployeeRepository
    {
        public EmployeeRepository(EmployeeDbContext context) : base(context)
        {
        }

    }
    
}
