using System;


namespace EmployeeAdminPortal.DataModel
{
    public class Employee
    {
        public Guid id { get; set; }
        public string FirstName{ get; set; }
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }

        public long Mobile { get; set; }

        public string ProfileImageUrl { get; set; } = string.Empty;

        public Guid GenderId { get; set; }

        //Navigation Properties
        public Gender Gender { get; set; }
        public Address Address { get; set; }


    }
}
