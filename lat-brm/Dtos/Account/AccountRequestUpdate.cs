using lat_brm.Models;

namespace lat_brm.Dtos.Account
{
    public class AccountRequestUpdate
    {
        public Guid Guid { get; set; }
        public string Password { get; set; } = null!;
        public ulong? IsDeleted { get; set; }
        public int? Otp { get; set; }
        public ulong? IsUsed { get; set; }
        public DateTime? ExpiredTime { get; set; }
    
        public static implicit operator TbMAccount(AccountRequestUpdate accountRequestUpdate)
        {
            return new TbMAccount
            {
                Guid = accountRequestUpdate.Guid,
                Password = accountRequestUpdate.Password,
                IsDeleted = accountRequestUpdate.IsDeleted,
                Otp = accountRequestUpdate.Otp,
                IsUsed = accountRequestUpdate.IsUsed,
                ExpiredTime = accountRequestUpdate.ExpiredTime,
                ModifiedDate = DateTime.Now,
            };
        }
    
    }
}
