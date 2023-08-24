using lat_brm.Models;

namespace lat_brm.Dtos.AccountRole
{
    public class AccountRoleRequestInsert
    {
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }

        public static implicit operator TbMAccountRole(AccountRoleRequestInsert accountRoleRequest)
        {
            return new TbMAccountRole
            {
                Guid = Guid.NewGuid(),
                AccountGuid = accountRoleRequest.AccountGuid,
                RoleGuid = accountRoleRequest.RoleGuid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

        }
    }
}
