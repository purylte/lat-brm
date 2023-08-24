using lat_brm.Models;

namespace lat_brm.Dtos.Employee
{
    public class EmployeeRequestInsert
    {
        public string Nik { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public int Gender { get; set; }
        public DateTime HiringDate { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

        public static implicit operator TbMEmployee(EmployeeRequestInsert employeeRequestInsert)
        {
            return new TbMEmployee
            {
                Guid = Guid.NewGuid(),
                Nik = employeeRequestInsert.Nik,
                FirstName = employeeRequestInsert.FirstName,
                LastName = employeeRequestInsert.LastName,
                BirthDate = employeeRequestInsert.BirthDate,
                Gender = employeeRequestInsert.Gender,
                HiringDate = employeeRequestInsert.HiringDate,
                Email = employeeRequestInsert.Email,
                PhoneNumber = employeeRequestInsert.PhoneNumber,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
        }
    }
}
