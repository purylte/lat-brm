using lat_brm.Models;

namespace lat_brm.Dtos.Role
{
    public class RoleRequestUpdate
    {
        public string Name { get; set; } = null!;
        public Guid Guid { get; set; }
    
        public static implicit operator TbMRole(RoleRequestUpdate roleRequest)
        {
            return new TbMRole
            {
                Guid = roleRequest.Guid,
                Name = roleRequest.Name,
                ModifiedDate = DateTime.Now,
            };
        }
    }


}
