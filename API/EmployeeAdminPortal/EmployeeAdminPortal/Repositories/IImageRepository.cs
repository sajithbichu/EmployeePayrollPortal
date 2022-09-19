using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace EmployeeAdminPortal.Repositories
{
    public interface IImageRepository
    {
        Task<string> Upload(IFormFile file, string fileName);
    }
}
