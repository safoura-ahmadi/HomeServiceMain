using HomeService.Domain.Core.Contracts.Repository.BaseEntities;
using HomeService.Domain.Core.Contracts.Service.BaseEntities;
using HomeService.Domain.Core.Entities;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace HomeService.Domain.Service.Services.BaseEntity;

public class ImageService(IImageRepository repository) : IImageReposiotry
{
    private readonly IImageRepository _repository = repository;

    public async Task<Result> Create(List<string> imgAddress, int orderId, CancellationToken cancellationToken)
    {
        if (orderId <= 0)
            return Result.Fail("سفارشی با این مشخصات برای آپلود عکس وجود ندارد");
        if (imgAddress.Count == 0)
            return Result.Fail("آدرس عکس ها برای آپلود نامعتبر است");
        var result = await _repository.Create(imgAddress, orderId, cancellationToken);
        if (result)
            return Result.Ok("عکس های سفارش با موفقیت آپلود شد");
        return Result.Fail("در فراید آپلود عکس دیتا بیس دچار مشکل شده است");
    }

    public async Task<string> UploadImage(IFormFile FormFile, string folderName, CancellationToken cancellation)
    {
        string filePath;
        string fileName;
        if (FormFile != null)
        {
            fileName = Guid.NewGuid().ToString() +
                       ContentDispositionHeaderValue.Parse(FormFile.ContentDisposition).FileName.Trim('"');
            filePath = Path.Combine($"wwwroot/UserTemplate/images/{folderName}", fileName);
            try
            {
                using (var stream = System.IO.File.Create(filePath))
                {
                    await FormFile.CopyToAsync(stream, cancellation);
                }
            }
            catch
            {
                throw new Exception("Upload files operation failed");
            }
            return $"{fileName}";
        }
        else
            fileName = "";

        return fileName;
    }


}
