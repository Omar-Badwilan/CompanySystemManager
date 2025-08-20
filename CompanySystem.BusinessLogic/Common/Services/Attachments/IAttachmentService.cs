using Microsoft.AspNetCore.Http;

namespace CompanySystem.BusinessLogic.Common.Services.Attachments
{
    public interface IAttachmentService
    {
        string? Upload(IFormFile file, string foldarName);

        bool Delete(string filePath);
    }
}
