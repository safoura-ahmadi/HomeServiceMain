using HomeService.Domain.Core.Contracts.AppService.Categories;
using HomeService.Domain.Core.Contracts.Service.BaseEntities;
using HomeService.Domain.Core.Contracts.Service.Categories;
using HomeService.Domain.Core.Dtos.Categories;
using HomeService.Domain.Core.Entities;
using Microsoft.Extensions.Logging;

namespace HomeService.Domain.Service.AppServices.Categories;

public class SubServiceAppService(ISubServiceService subService, IImageReposiotry imageService, ILogger<SubServiceAppService> logger) : ISubServiceAppService
{
    private readonly ISubServiceService _subService = subService;
    private readonly IImageReposiotry _imageService = imageService;
    private readonly ILogger<SubServiceAppService> _logger = logger;

    public async Task<Result> Create(CreateSubServiceDto model, CancellationToken cancellationToken)
    {

        var result = await _subService.Create(model, cancellationToken);
        if (result.Success)
        {
            if (model.ImageFile is not null)
                model.ImagePath = await _imageService.UploadImage(model.ImageFile!, "icon", cancellationToken);

        }
        return result;


    }

    public async Task<Result> Delete(int id, CancellationToken cancellationToken)
    {
        _logger.Log(LogLevel.Warning, "Attempt to delete{item}", "SubService");
        if (id <= 0)
            return Result.Fail("هوم سرویسی با این ایدی موجود نیست");
        return await _subService.Delete(id, cancellationToken);
    }

    public async Task<List<GetSubServiceDto>> GetAll(int pageNumber, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0)
            pageNumber = 1;
        return await _subService.GetAll(pageNumber, 10, cancellationToken);
    }

    public async Task<UpdateSubServiceDto?> GetById(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return null;
         return await _subService.GetById(id, cancellationToken);
    }

    public async Task<List<GetSubServiceDto>> GetBySubCategoryId(int subcategoryId, CancellationToken cancellationToken)
    {
        return await _subService.GetBySubCategoryId(subcategoryId, cancellationToken);
    }

    public async Task<int> GetTotalConut(CancellationToken cancellationToken)
    {
        return await _subService.GetTotalCount(cancellationToken);
    }

    public async Task<List<GetSubServiceDto>> Search(string text, CancellationToken cancellationToken)
    {
        return await _subService.Search(text, cancellationToken);
    }

    public async Task<Result> Update(UpdateSubServiceDto model, CancellationToken cancellationToken)
    {
        if (model.Id <= 0)
            return Result.Fail("هوم سرویسی با این ایدی موجود نیست");
        return await _subService.Update(model, cancellationToken);
    }
}
