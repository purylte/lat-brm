using lat_brm.Contracts.Repositories;
using lat_brm.Data;
using lat_brm.Models;

namespace lat_brm.Repositories
{
    public class EducationRepository : GeneralRepository<TbMEducation>, IEducationRepository
    {
        public EducationRepository(EmployeeDbContext context) : base(context)
        {
        }

    }
}
