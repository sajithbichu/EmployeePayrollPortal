using AutoMapper;
using DataModel = EmployeeAdminPortal.DataModel;
using EmployeeAdminPortal.DomainModels;
using EmployeeAdminPortal.Profiles.AfterMaps;

namespace EmployeeAdminPortal.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<DataModel.Employee, Employee>()
                 .ReverseMap();

            CreateMap<DataModel.Gender, Gender>()
                 .ReverseMap();

            CreateMap<DataModel.Address, Address>()
                 .ReverseMap();

            CreateMap<UpdateEmployeeRequest, DataModel.Employee>()
                .AfterMap<UpdateEmployeeRequestAfterMap>();

            CreateMap<AddEmployeeRequest, DataModel.Employee>()
                .AfterMap<AddEmployeeRequestAfterMap>();

        }

    }
}
