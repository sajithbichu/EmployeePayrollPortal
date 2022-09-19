using EmployeeAdminPortal.DomainModels;
using EmployeeAdminPortal.Repositories;
using FluentValidation;

namespace EmployeeAdminPortal.Validators
{
    public class UpdateEmployeeRequestValidator : AbstractValidator<UpdateEmployeeRequest>
    {
        public UpdateEmployeeRequestValidator(IEmployeeRepository EmployeeRepository)
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.DateOfBirth).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Mobile).NotEmpty();
            RuleFor(x => x.GenderId).NotEmpty().Must(id =>
            {
                var gender = EmployeeRepository.GetGendersAsync().Result.ToList().FirstOrDefault(x => x.id == id);

                if (gender != null)
                {
                    return true;
                }

                return false;
            }).WithMessage("Please select a valid Gender");
            RuleFor(x => x.PhysicalAddress).NotEmpty();
            RuleFor(x => x.PostalAddress).NotEmpty();
        }
    }
}
