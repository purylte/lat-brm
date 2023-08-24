using lat_brm.Models;

namespace lat_brm.Dtos.Account
{
    public class AccountRequestInsert
    {
        public Guid Guid { get; set; }
        public string Password { get; set; }  = null!;
        public ulong? IsDeleted { get; set; }
        public int? Otp { get; set; }
        public ulong? IsUsed { get; set; }
        public DateTime? ExpiredTime { get; set; }

        public static implicit operator TbMAccount(AccountRequestInsert accountRequest)
        {
            return new TbMAccount
            {
                Guid = accountRequest.Guid,
                Password = accountRequest.Password,
                IsDeleted = accountRequest.IsDeleted,
                Otp = accountRequest.Otp,
                IsUsed = accountRequest.IsUsed,
                ExpiredTime = accountRequest.ExpiredTime,
                CreatedDate = System.DateTime.Now,
                ModifiedDate = System.DateTime.Now
            };
        }
    }
}
