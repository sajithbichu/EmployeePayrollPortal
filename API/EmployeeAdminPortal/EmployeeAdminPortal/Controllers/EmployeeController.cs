using AutoMapper;
using EmployeeAdminPortal.DomainModels;
using EmployeeAdminPortal.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace EmployeeAdminPortal.Controllers
{
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository EmployeeRepository;
        private readonly IMapper mapper;
        private readonly IImageRepository imageRepository;

        //Constructor to bind or intialize Employee Repositor, mapers and image repositories
        public EmployeeController(IEmployeeRepository EmployeeRepository, IMapper mapper, IImageRepository imageRepository)
        {
            this.EmployeeRepository = EmployeeRepository;
            this.mapper = mapper;
            this.imageRepository = imageRepository;
        }

        //Get All employees Web API defenition using auto mapper.
        [HttpGet]
        [Route("[Controller]")]
        public async Task<IActionResult> GetAllEmployees()
        {
            //return  Ok(EmployeeRepository.GetEmployees());

            // Binding Data model to Doman model. As we only expose Domanin model from API. Not Data Model
            var _employees = await EmployeeRepository.GetEmployeesAsync();

            //var domainModelEmployees = new List<Employee>();

            //foreach (var employee in employees)
            //{
            //    domainModelEmployees.Add(new Employee()
            //    {
            //        id = employee.id,
            //        FirstName = employee.FirstName,
            //        LastName = employee.LastName,
            //        DateOfBirth = employee.DateOfBirth,
            //        Email = employee.Email,
            //        Mobile = employee.Mobile,
            //        ProfileImageUrl = employee.ProfileImageUrl,
            //        GenderId = employee.GenderId,
            //        Address = new Address()
            //        {
            //            id = employee.Address.id,
            //            PhysicalAddress = employee.Address.PhysicalAddress,
            //            PostalAddress = employee.Address.PostalAddress,
            //        },
            //        Gender = new Gender()
            //        {
            //            id = employee.Gender.id,
            //            Description = employee.Gender.Description,
            //        }
            //    });
            //}

            return Ok(mapper.Map<List<Employee>>(_employees));
        }
        
        //Get employee details by Employee ID Web API.
        [HttpGet]
        [Route("[Controller]/{employeeId:guid}"), ActionName("GetEmployeeByID")]
        public async Task<IActionResult> GetEmployeeByID([FromRoute] Guid employeeId)
        {
            var _employees = await EmployeeRepository.GetEmployeeAsync(employeeId);

            if (_employees == null)
                return NotFound();

            return Ok(mapper.Map<Employee>(_employees));

        }

        // Update employees API
        [HttpPut]
        [Route("[Controller]/{employeeId:guid}")]
        public async Task<IActionResult> UpdateEmployeeAsync([FromRoute] Guid employeeId, [FromBody] UpdateEmployeeRequest request)
        {
            if (await EmployeeRepository.Exists(employeeId))
            {
                //Update Details
                var _updatedEmployee = await EmployeeRepository.updateEmployee(employeeId, mapper.Map<DataModel.Employee>(request));

                if (_updatedEmployee != null)
                {
                    return Ok(mapper.Map<Employee>(_updatedEmployee));
                }
            }

            return NotFound();

        }

        //Delete employee records using Web API.
        [HttpDelete]
        [Route("[Controller]/{employeeId:guid}")]
        public async Task<IActionResult> DeleteEmployeeAsync([FromRoute] Guid employeeId)
        {
            if (await EmployeeRepository.Exists(employeeId))
            {
                var _employee = await EmployeeRepository.DeleteEmployee(employeeId);
                return Ok(mapper.Map<Employee>(_employee));
            }
            return NotFound();
        }

        //Add or create employee records web API

        [HttpPost]
        [Route("[Controller]/Add")]
        public async Task<IActionResult> AddEmployeeAsync([FromBody] AddEmployeeRequest request)
        {
            var employee = await EmployeeRepository.AddEmployee(mapper.Map<DataModel.Employee>(request));
            return CreatedAtAction(nameof(GetEmployeeByID), new { employeeId = employee.id }, 
                mapper.Map<Employee>(employee));
        }

        // Upload Image Web API
        [HttpPost]
        [Route("[Controller]/{employeeId:guid}/upload-image")]
        public async Task<IActionResult> UploadImage([FromRoute] Guid employeeId, IFormFile profileImage)
        {

            var validExtensions = new List<string>
            {
                ".jpeg",
                ".png",
                ".gif",
                ".jpg"
            };
            if (profileImage!=null && profileImage.Length > 0)
            {
                string extension = Path.GetExtension(profileImage.FileName);

                if (validExtensions.Contains(extension.ToString().ToLower()))
                {

                    if (await EmployeeRepository.Exists(employeeId))
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName);
                        var fileImagePath = await imageRepository.Upload(profileImage, fileName);

                        if (await EmployeeRepository.UpdateProfileImage(employeeId, fileImagePath))
                        {

                            return Ok(fileImagePath);
                        }

                        return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading Image");
                    }
                }

                return BadRequest("This is not a valid Image fomat");
                    
            }

            return NotFound();
        }



    }
}
