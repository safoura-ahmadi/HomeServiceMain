using HomeService.Domain.Core.Contracts.AppService.Categories;
using HomeService.Domain.Core.Contracts.Service.BaseEntities;
using HomeService.Domain.Core.Contracts.Service.Categories;
using HomeService.Domain.Core.Dtos.Categories;
using HomeService.Domain.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace HomeService.Domain.Service.AppServices.Categories;

public class CategoryAppService(ICategoryService categoryService, IImageReposiotry imageService, ILogger<CategoryAppService> logger) : ICategoryAppService
{
    private readonly ICategoryService _categoryService = categoryService;
    private readonly IImageReposiotry _imageService = imageService;
    private readonly ILogger<CategoryAppService> _logger = logger;

    public async Task<Result> Create(string title, IFormFile imageFile, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(title))
            return Result.Fail("عنوان وارد شده نامعتبر است");
        if (imageFile is null)
            return Result.Fail("ادرس عکس اپلود شده نامعتبر است");
        string imagePath = string.Empty;
        try
        {
            if (imageFile is not null)
            {
                imagePath = await _imageService.UploadImage(imageFile, "icon", cancellationToken);
            }


        }
        catch
        {
            Result.Fail("مشکلی در اپلود عکس بوجود آمده است");
        }

        return await _categoryService.Create(title, imagePath, cancellationToken);
    }

    public async Task<Result> Delete(int id, CancellationToken cancellationToken)
    {
        _logger.Log(LogLevel.Warning, "Attempt to delete{item}", "Category");
        if (id <= 0)
            return Result.Fail("کتگوری با این مشخصات وجود ندارد");

        return await _categoryService.Delete(id, cancellationToken);

    }

    public async Task<List<GetCategoryForAdminPageDto>> GetAll(CancellationToken cancellationToken)
    {
        return await _categoryService.GetAll(cancellationToken);
    }

    public async Task<List<GetCategoryForMainPageDto>> GetAllForMainPage(CancellationToken cancellationToken)
    {
        return await _categoryService.GetAllForMainPage(cancellationToken);
    }

    public async Task<UpdateCategoryDto?> GetById(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return null;
        return await _categoryService.GetById(id, cancellationToken);
    }

    public async Task<Result> Update(int id, string title, IFormFile imageFile, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return Result.Fail("ایدی دسته بندی نامعتبر است");
        if (string.IsNullOrEmpty(title))
            return Result.Fail("عنوان وارد شده نامعتبر است");
        if (imageFile is null)
            return Result.Fail("ادرس عکس اپلود شده نامعتبر است");
        string imagePath = string.Empty;
        try
        {
            if (imageFile is not null)
            {
                imagePath = await _imageService.UploadImage(imageFile, "icon", cancellationToken);
            }


        }
        catch
        {
            Result.Fail("مشکلی در اپلود عکس بوجود آمده است");
        }
        return await _categoryService.Update(id, title, imagePath, cancellationToken);

    }

    public async Task<Result> Update(UpdateCategoryDto model, CancellationToken cancellationToken)
    {
        if (model.Id <= 0)
            return Result.Fail("کتگوری با این مشخصات یافت نشد");
        if (string.IsNullOrEmpty(model.Title))
            return Result.Fail("عنوان وارد شده نامعتبر است");

        try
        {
            if (model.ImageFile is not null)
            {
                model.ImagePath = await _imageService.UploadImage(model.ImageFile, "icon", cancellationToken);
            }


        }
        catch
        {
            Result.Fail("مشکلی در اپلود عکس بوجود آمده است");
        }
        return await _categoryService.Update(model, cancellationToken);
    }
}
