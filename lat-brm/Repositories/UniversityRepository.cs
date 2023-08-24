using lat_brm.Contracts;
using lat_brm.Data;
using lat_brm.Models;

namespace lat_brm.Repositories
{
    public class UniversityRepository : GeneralRepository<TbMUniversity>, IUniversityRepository
    {
        private readonly EmployeeDbContext _context;
        public UniversityRepository(EmployeeDbContext context) : base(context)
        {
            _context = context;
        }

        public TbMUniversity? GetByCodeAndName(string code, string name)
        {
            var university = _context.Set<TbMUniversity>().FirstOrDefault(u => u.Code == code && u.Name == name);
            return university;
        }
    }
}
