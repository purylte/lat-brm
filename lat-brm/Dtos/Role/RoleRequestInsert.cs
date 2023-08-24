using lat_brm.Models;

namespace lat_brm.Dtos.Role
{
    public class RoleRequestInsert
    {

        public string Name { get; set; } = null!;

        public static implicit operator TbMRole(RoleRequestInsert roleRequest)
        {
            return new TbMRole
            {
                Guid = Guid.NewGuid(),
                Name = roleRequest.Name,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };

        }
    }
}
