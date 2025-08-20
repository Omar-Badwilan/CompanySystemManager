using Microsoft.AspNetCore.Http;

namespace CompanySystem.BusinessLogic.Common.Services.Attachments
{
    public class AttachmentService : IAttachmentService
    {
        private readonly List<string> _allowedExtensions = new() { ".png", ".jpg", ".jpeg" };
        private const int _allowedMaxSize = 2_097_152;
        public string? Upload(IFormFile file, string foldarName)
        {
            var extension = Path.GetExtension(file.FileName);

            if (!_allowedExtensions.Contains(extension))
                return null;
            if (file.Length > _allowedMaxSize)
                return null;

            //var foldarPath = $"{Directory.GetCurrentDirectory}\\wwwroot\\files\\{foldarName}";

            var foldarPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", foldarName);

            if(!Directory.Exists(foldarPath))
                Directory.CreateDirectory(foldarPath);

            var fileName = $"{Guid.NewGuid()}-{extension}"; // UNIQUE

            var filePath = Path.Combine(foldarPath, fileName); // File Location   

            //streaming :data per time
            // using for dispose

            //using var fileStream = new FileStream(filePath, FileMode.Create);

            using var fileStream = File.Create(filePath);

            file.CopyTo(fileStream);

            return fileName;
        }

        public bool Delete(string filePath)
        {
            if(File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }

    }
}
