using Microsoft.AspNetCore.Http;

namespace CompanySystem.BusinessLogic.Common.Services.Attachments
{
    internal interface IAttachmentService
    {
        string? Upload(IFormFile file, string foldarName);

        bool Delete(string filePath);
    }
}
