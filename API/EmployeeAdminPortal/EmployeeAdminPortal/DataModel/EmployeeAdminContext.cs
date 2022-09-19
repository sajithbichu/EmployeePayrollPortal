using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortal.DataModel
{
    public class EmployeeAdminContext : DbContext
    {
        public EmployeeAdminContext(DbContextOptions<EmployeeAdminContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Gender> Gender { get; set; }   
        public DbSet<Address> Address   { get; set; }

    }
}
