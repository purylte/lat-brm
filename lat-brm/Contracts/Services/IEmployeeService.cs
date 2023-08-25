using lat_brm.Dtos.Employee;

namespace lat_brm.Contracts.Services
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeResponse> GetAll();
        EmployeeResponse? GetById(Guid id);
        EmployeeResponse? Insert(EmployeeRequestInsert request);
        EmployeeResponse? Update(EmployeeRequestUpdate request);
        bool Delete(Guid id);
        IEnumerable<EmployeeResponseInfo>? GetInfos();
        EmployeeResponseInfo? GetInfoById(Guid id);
    }
}
