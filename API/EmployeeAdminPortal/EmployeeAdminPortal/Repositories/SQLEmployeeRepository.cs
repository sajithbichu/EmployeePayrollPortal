using EmployeeAdminPortal.DataModel;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortal.Repositories
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeAdminContext context;
        public SQLEmployeeRepository(EmployeeAdminContext context)
        {
            this.context = context;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await context.Employee.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync();
        }

        public async Task<Employee> GetEmployeeAsync(Guid employeeId)
        {
            return await context.Employee.Include(nameof(Gender)).Include(nameof(Address)).FirstOrDefaultAsync(x => x.id == employeeId);
        }

        public async Task<List<Gender>> GetGendersAsync()
        {
            return await context.Gender.ToListAsync();
        }

        public async Task<bool> Exists(Guid employeeId)
        {
            return await context.Employee.AnyAsync(x => x.id == employeeId);
        }

        public async Task<Employee> updateEmployee(Guid employeeId, Employee request)
        {
            var existingEmployee = await GetEmployeeAsync(employeeId);
            if (existingEmployee !=null)
            {
                existingEmployee.FirstName = request.FirstName;
                existingEmployee.LastName = request.LastName;
                existingEmployee.DateOfBirth = request.DateOfBirth;
                existingEmployee.Email = request.Email;
                existingEmployee.Mobile = request.Mobile;
                existingEmployee.GenderId = request.GenderId;
                existingEmployee.Address.PhysicalAddress = request.Address.PhysicalAddress;
                existingEmployee.Address.PostalAddress = request.Address.PostalAddress;
                await context.SaveChangesAsync();
                return existingEmployee;
            }

            return null;
        }

        public async Task<Employee> DeleteEmployee(Guid employeeId)
        {
            var existingEmployee = await GetEmployeeAsync(employeeId);

            if (existingEmployee != null)
            {
                context.Remove(existingEmployee);
                await context.SaveChangesAsync();
                return existingEmployee;
            }
            return null;
        }

        public async Task<Employee> AddEmployee(Employee request)
        {
            var employee = await context.Employee.AddAsync(request);
            await context.SaveChangesAsync();
            return employee.Entity;
        }

        public async Task<bool> UpdateProfileImage(Guid employeeId, string profileImageUrl)
        {
            var employee = await GetEmployeeAsync(employeeId);

            if (employee != null)
            {
                employee.ProfileImageUrl = profileImageUrl;
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
