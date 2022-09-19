namespace EmployeeAdminPortal.DomainModels
{
    public class Address
    {
        public Guid id { get; set; }
        public string PhysicalAddress { get; set; }
        public string PostalAddress { get; set; }
        public Guid EmployeeId { get; set; }


    }
}
