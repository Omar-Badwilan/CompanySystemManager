using Microsoft.AspNetCore.Http;

namespace CompanySystem.BusinessLogic.Common.Services.Attachments
{
    public interface IAttachmentService
    {
        Task<string?> UploadFileAsync(IFormFile file, string foldarName);

        bool Delete(string filePath);
    }
}
