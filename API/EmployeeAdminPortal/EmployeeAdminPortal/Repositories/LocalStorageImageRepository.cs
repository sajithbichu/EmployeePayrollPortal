
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.IO;


namespace EmployeeAdminPortal.Repositories
{
    public class LocalStorageImageRepository : IImageRepository
    {
        public async Task<string> Upload(IFormFile file, string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Images", fileName);
            using Stream fileStream = new FileStream(path, FileMode.Create);

            await file.CopyToAsync(fileStream);
            return GetServerRelativePath(fileName);
        }
        private string GetServerRelativePath(String fileName)
        {
            return Path.Combine(@"Resources\Images", fileName);
        }
    }
}
