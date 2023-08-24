using lat_brm.Models;

namespace lat_brm.Dtos.AccountRole
{
    public class AccountRoleRequestUpdate
    {
        public Guid Guid { get; set; }
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }

        public static implicit operator TbMAccountRole(AccountRoleRequestUpdate accountRoleRequest)
        {
            return new TbMAccountRole
            {
                Guid = accountRoleRequest.Guid,
                AccountGuid = accountRoleRequest.AccountGuid,
                RoleGuid = accountRoleRequest.RoleGuid,
                ModifiedDate = DateTime.Now,
            };

        }
    }
}
