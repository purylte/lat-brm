using lat_brm.Models;

namespace lat_brm.Dtos.Account
{
    public class AccountRequestRegister
    {
        public string Nik { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public int Gender { get; set; }
        public DateTime HiringDate { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Major { get; set; } = null!;
        public string Degree { get; set; } = null!;
        public double Gpa { get; set; }
        public string UniversityCode { get; set; } = null!;
        public string UniversityName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }
}
