using Microsoft.AspNetCore.Http;

namespace HomeService.Domain.Core.Contracts.Service.BaseEntities;

public interface IImageService
{
    Task<string> UploadImage(IFormFile FormFile, string folderName, CancellationToken cancellationToken);
}
