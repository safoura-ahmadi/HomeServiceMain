
using HomeService.Domain.Core.Contracts.AppService.Categories;
using HomeService.Domain.Core.Contracts.AppService.EndPoint;
using HomeService.Domain.Core.Contracts.Service.BaseEntities;
using HomeService.Domain.Core.Contracts.Service.Categories;
using HomeService.Domain.Core.Dtos.Categories;
using HomeService.Domain.Core.Entities;
using HomeService.Domain.Core.Entities.Categories;

namespace HomeService.Domain.Service.AppServices.EndPoint;

public class AdminSubserviceManagement(ISubServiceService subServiceService, ISubCategoryService subCategoryService, IImageReposiotry imageService) : IAdminSubserviceManagement
{
    private readonly ISubServiceService _subServiceService = subServiceService;
    private readonly ISubCategoryService _subCategoryService = subCategoryService;
    private readonly IImageReposiotry _imageService = imageService;

    public async Task<Result> Create(CreateSubServiceDto model, CancellationToken cancellationToken)
    {

        try
        {
            if (model.ImageFile is not null)
                model.ImagePath = await _imageService.UploadImage(model.ImageFile!, "icon", cancellationToken);


        }
        catch
        {
            Result.Fail("مشکلی در اپلود عکس بوجود آمده است");
        }
        var result = await _subServiceService.Create(model, cancellationToken);
        return result;

    }

    public async Task<List<GetSubCategoryDto>> GetAllSubCategories(CancellationToken cancellationToken)
    {
        return await _subCategoryService.GetAll(cancellationToken);
    }



    public async Task<List<GetSubServiceDto>> GetAllSubservices(int pageNumber, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0)
            pageNumber = 1;
        return await _subServiceService.GetAll(pageNumber, 10, cancellationToken);
    }

    public async Task<UpdateSubServiceDto?> GetById(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return null;
        return await _subServiceService.GetById(id, cancellationToken);
    }

    public async Task<int> GetTotalConut(CancellationToken cancellationToken)
    {
        return await _subServiceService.GetTotalCount(cancellationToken);
    }

    public async Task<Result> Update(UpdateSubServiceDto model, CancellationToken cancellationToken)
    {
        if (model.Id <= 0)
            return Result.Fail("هوم سرویسی با این مشخصات یافت نشد");
        try
        {
            if (model.ImageFile is not null)
                model.ImagePath = await _imageService.UploadImage(model.ImageFile!, "icon", cancellationToken);


        }
        catch
        {
            Result.Fail("مشکلی در اپلود عکس بوجود آمده است");
        }
        return await _subServiceService.Update(model, cancellationToken);
    }
}
