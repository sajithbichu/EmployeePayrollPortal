namespace EmployeeAdminPortal.DataModel
{
    public class Address
    {
        public Guid id { get; set; }
        public string PhysicalAddress { get; set; }
        public string PostalAddress { get; set; }

        // Navigation Property
        public Guid EmployeeId { get; set; }


    }
}
