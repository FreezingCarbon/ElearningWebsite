using Microsoft.AspNetCore.Http;

namespace ElearningWebsite.API.Helpers
{
    public class FormFileChecker
    {
        public FormFileChecker() { }
        public bool CheckFileType(IFormFile file, string fileType)
        {
            return file.ContentType.ToString().StartsWith(fileType);
        }
    }
}