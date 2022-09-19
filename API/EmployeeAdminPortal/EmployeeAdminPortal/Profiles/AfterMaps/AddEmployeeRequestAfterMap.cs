using AutoMapper;
using EmployeeAdminPortal.DomainModels;

namespace EmployeeAdminPortal.Profiles.AfterMaps
{
    public class AddEmployeeRequestAfterMap : IMappingAction<AddEmployeeRequest, DataModel.Employee>
    {
        public void Process(AddEmployeeRequest source, DataModel.Employee destination, ResolutionContext context)
        {
            destination.id = Guid.NewGuid();
            destination.Address = new DataModel.Address()
            {
                id = Guid.NewGuid(),
                PhysicalAddress=source.PhysicalAddress,
                PostalAddress=source.PostalAddress
            };

        }
    }
}
