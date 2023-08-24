using lat_brm.Models;

namespace lat_brm.Dtos.Role
{
    public class RoleResponse
    {
        public Guid Guid { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public static explicit operator RoleResponse(TbMRole role)
        {
            return new RoleResponse
            {
                Guid = role.Guid,
                Name = role.Name,
                ModifiedDate = role.ModifiedDate,
                CreatedDate = role.CreatedDate
            };
        }
    }
}
