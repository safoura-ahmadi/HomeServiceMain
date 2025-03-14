using HomeService.Domain.Core.Contracts.AppService.Categories;
using HomeService.Domain.Core.Contracts.Service.Categories;
using HomeService.Domain.Core.Dtos.Categories;

namespace HomeService.Domain.Service.AppServices.Categories;

public class SubCategoryAppService(ISubCategoryService subCategoryService) : ISubCategoryAppService
{
    private readonly ISubCategoryService _subCategoryService = subCategoryService;

    public async Task<List<GetSubCategoryDto>> GetAll(CancellationToken cancellationToken)
    {
        return await _subCategoryService.GetAll(cancellationToken);
    }

    public async Task<List<GetAllSubCategoryWithServiceDto>> GetAllWithService(CancellationToken cancellationToken)
    {
        return await _subCategoryService.GetAllWithService(cancellationToken);
    }
}
