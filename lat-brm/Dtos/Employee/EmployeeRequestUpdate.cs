using lat_brm.Models;

namespace lat_brm.Dtos.Employee
{
    public class EmployeeRequestUpdate
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

        public static implicit operator TbMEmployee(EmployeeRequestUpdate employeeRequestInsert)
        {
            return new TbMEmployee
            {
                Guid = employeeRequestInsert.Guid,
                Nik = employeeRequestInsert.Nik,
                FirstName = employeeRequestInsert.FirstName,
                LastName = employeeRequestInsert.LastName,
                BirthDate = employeeRequestInsert.BirthDate,
                Gender = employeeRequestInsert.Gender,
                HiringDate = employeeRequestInsert.HiringDate,
                Email = employeeRequestInsert.Email,
                PhoneNumber = employeeRequestInsert.PhoneNumber,
                ModifiedDate = DateTime.Now,
            };
        }
    }
}
