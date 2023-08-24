using lat_brm.Models;

namespace lat_brm.Dtos.Account
{
    public class AccountResponse
    {
        public Guid Guid { get; set; }
        public ulong? IsDeleted { get; set; }
        public int? Otp { get; set; }
        public ulong? IsUsed { get; set; }
        public DateTime? ExpiredTime { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    
        public static explicit operator AccountResponse(TbMAccount account)
        {
            return new AccountResponse
            {
                Guid = account.Guid,
                IsDeleted = account.IsDeleted,
                Otp = account.Otp,
                IsUsed = account.IsUsed,
                ExpiredTime = account.ExpiredTime,
                CreatedDate = account.CreatedDate,
                ModifiedDate = account.ModifiedDate
            };
        }
    }


}
