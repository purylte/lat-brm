using lat_brm.Dtos.Account;

namespace lat_brm.Contracts.Services
{
    public interface IAccountService
    {
        public IEnumerable<AccountResponse> GetAll();
        public AccountResponse? GetById(Guid id);
        public AccountResponse? Insert(AccountRequestInsert request);
        public AccountResponse? Update(AccountRequestUpdate request);
        public bool Delete(Guid id);
        public AccountResponseAuthenticate? Register(AccountRequestRegister request);
        public AccountResponseAuthenticate? Login(AccountRequestLogin request);
    }
}
