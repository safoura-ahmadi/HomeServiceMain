using HomeService.Domain.Core.Contracts.AppService.Categories;
using HomeService.Domain.Core.Contracts.Service.Categories;
using HomeService.Domain.Core.Dtos.Categories;
using HomeService.Domain.Core.Entities;

namespace HomeService.Domain.Service.AppServices.Categories;

public class CategoryAppService(ICategoryService categoryService) : ICategoryAppService
{
    private readonly ICategoryService _categoryService = categoryService;

    public async Task<Result> Create(string title, string imagePath, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(title))
            return Result.Fail("عنوان وارد شده نامعتبر است");
        if (string.IsNullOrEmpty(imagePath) || imagePath.Length > 500)
            return Result.Fail("ادرس عکس اپلود شده نامعتبر است");
        return await _categoryService.Create(title, imagePath, cancellationToken);
    }

    public Task<Result> Delete(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<List<GetCategoryForAdminPageDto>> GetAll(CancellationToken cancellationToken)
    {
        return await _categoryService.GetAll(cancellationToken);
    }

    public async Task<Result> Update(int id, string title, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return Result.Fail("ایدی دسته بندی نامعتبر است");
        if (string.IsNullOrEmpty(title))
            return Result.Fail("عنوان وارد شده نامعتبر است");
        return await _categoryService.Update(id, title, cancellationToken);

    }
}
