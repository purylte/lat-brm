using lat_brm.Models;

namespace lat_brm.Dtos.Employee
{
    public class EmployeeResponse
    {
        public Guid Guid { get; set; }
        public string? Nik { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? Gender { get; set; }
        public DateTime? HiringDate { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public static explicit operator EmployeeResponse(TbMEmployee employee)
        {
            return new EmployeeResponse
            {
                Guid = employee.Guid,
                Nik = employee.Nik,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                BirthDate = employee.BirthDate,
                Gender = employee.Gender,
                HiringDate = employee.HiringDate,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                CreatedDate = employee.CreatedDate,
                ModifiedDate = employee.ModifiedDate
            };
        }
    }

}
