using HomeService.Domain.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace HomeService.Domain.Core.Contracts.Service.BaseEntities;

public interface IImageReposiotry
{
    Task<string> UploadImage(IFormFile FormFile, string folderName, CancellationToken cancellationToken);
    Task<Result> Create(List<string> imgAddress, int orderId, CancellationToken cancellationToken);
}
