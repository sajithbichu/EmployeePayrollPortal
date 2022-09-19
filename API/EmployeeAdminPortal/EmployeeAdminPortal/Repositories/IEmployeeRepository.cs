using EmployeeAdminPortal.DataModel;
using System.Collections.Generic;

namespace EmployeeAdminPortal.Repositories
{

    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployeesAsync();
        Task<Employee> GetEmployeeAsync(Guid employeeId);
        Task<List<Gender>> GetGendersAsync();
        Task<bool> Exists(Guid employeeId);
        Task<Employee> updateEmployee(Guid employeeId, Employee request);
        Task<Employee> DeleteEmployee(Guid employeeId);
        Task<Employee> AddEmployee(Employee request);
        Task<bool> UpdateProfileImage(Guid employeeId, string profileImageUrl);
    }

}
