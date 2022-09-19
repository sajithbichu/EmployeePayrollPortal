using AutoMapper;
using EmployeeAdminPortal.DomainModels;
using EmployeeAdminPortal.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    public class GendersController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GendersController(IEmployeeRepository _employeeRepository, IMapper _mapper)
        {
            this._employeeRepository = _employeeRepository;
            this._mapper = _mapper;
        }

        // Web API defenition to bind Gender list

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllGender()
        {
            var genderlist = await _employeeRepository.GetGendersAsync();

            if (genderlist == null || !genderlist.Any())
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<Gender>>(genderlist));
        }
    }
}
