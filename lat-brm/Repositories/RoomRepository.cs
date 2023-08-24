using lat_brm.Contracts;
using lat_brm.Data;
using lat_brm.Models;

namespace lat_brm.Repositories
{
    public class RoomRepository : GeneralRepository<TbMRoom>, IRoomRepository
    {
        public RoomRepository(EmployeeDbContext context) : base(context)
        {
        }
    }
    
    
}
