using lat_brm.Contracts;
using lat_brm.Data;
using lat_brm.Models;

namespace lat_brm.Repositories
{
    public class UniversityRepository : GeneralRepository<TbMUniversity>, IUniversityRepository
    {
        public UniversityRepository(EmployeeDbContext context) : base(context)
        {
        }
 
    }
}
