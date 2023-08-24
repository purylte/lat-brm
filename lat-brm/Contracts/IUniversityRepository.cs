using lat_brm.Models;

namespace lat_brm.Contracts
{
    public interface IUniversityRepository : IGeneralRepository<TbMUniversity>
    {
        public TbMUniversity? GetByCodeAndName(string code, string name);
    }
}
