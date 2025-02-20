using HomeService.Domain.Core.Contracts.AppService.Categories;
using HomeService.Domain.Core.Contracts.AppService.EndPoint;
using HomeService.Domain.Core.Contracts.Service.Categories;
using HomeService.Domain.Core.Dtos.Categories;
using HomeService.Domain.Core.Entities;

namespace HomeService.Domain.Service.AppServices.EndPoint;

public class AdminSubCategoryManagement(ICategoryService categoryService, ISubCategoryService subCategoryService) : IAdminSubCategoryManagement
{
    private readonly ICategoryService _categoryService = categoryService;
    private readonly ISubCategoryService _subCategoryService = subCategoryService;

    public async Task<Result> Create(string title, int CategoryId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(title))
            return Result.Fail("عنوان نامعتبر است");
        return await _subCategoryService.Create(title, CategoryId, cancellationToken);
    }

    public async Task<Result> Delete(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return Result.Fail("سابکتگوری با این مشخصات وجود ندارد");
        return await _subCategoryService.Delete(id, cancellationToken);
    }

    public async Task<List<GetCategoryForAdminPageDto>> GetAllCategories(CancellationToken cancellationToken)
    {
        return await _categoryService.GetAll(cancellationToken);
    }

    public async Task<List<GetSubCategoryDto>> GetAllSubCategories(CancellationToken cancellationToken)
    {
        return await _subCategoryService.GetAll(cancellationToken);
    }

    public async Task<UpdateSubCategoryDto?> GetById(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return null;
        return await _subCategoryService.GetById(id, cancellationToken);
    }

    public async Task<Result> Update(UpdateSubCategoryDto model, CancellationToken cancellationToken)
    {
        if (model.Id <= 0)
            return Result.Fail("سابکتگوری با این مشخصات وجود ندارد");
        return await _subCategoryService.Update(model, cancellationToken);
    }
}
