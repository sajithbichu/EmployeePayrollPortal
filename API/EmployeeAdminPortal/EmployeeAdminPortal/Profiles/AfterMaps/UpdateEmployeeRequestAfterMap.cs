using AutoMapper;
using EmployeeAdminPortal.DomainModels;
using DataModels = EmployeeAdminPortal.DataModel;

namespace EmployeeAdminPortal.Profiles.AfterMaps
{
    public class UpdateEmployeeRequestAfterMap : IMappingAction<UpdateEmployeeRequest, DataModels.Employee>
    {
        public void Process(UpdateEmployeeRequest source, DataModels.Employee destination, ResolutionContext context)
        {
            destination.Address = new DataModel.Address
            {
                PhysicalAddress = source.PhysicalAddress,
                PostalAddress = source.PostalAddress
            };
        }
    }
}
