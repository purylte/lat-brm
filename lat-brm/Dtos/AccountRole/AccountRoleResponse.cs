using lat_brm.Models;

namespace lat_brm.Dtos.AccountRole
{
    public class AccountRoleResponse
    {
        public Guid Guid { get; set; }
        public Guid? AccountGuid { get; set; }
        public Guid? RoleGuid { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    
        public static explicit operator AccountRoleResponse(TbMAccountRole accountRole)
        {
            return new AccountRoleResponse
            {
                Guid = accountRole.Guid,
                AccountGuid = accountRole.AccountGuid,
                RoleGuid = accountRole.RoleGuid,
                CreatedDate = accountRole.CreatedDate,
                ModifiedDate = accountRole.ModifiedDate
            };
        }
        
    }
}
