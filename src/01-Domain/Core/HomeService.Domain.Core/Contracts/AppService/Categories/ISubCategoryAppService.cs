using HomeService.Domain.Core.Dtos.Categories;

namespace HomeService.Domain.Core.Contracts.AppService.Categories;

public interface ISubCategoryAppService
{
    Task<List<GetAllSubCategoryWithServiceDto>> GetAllWithService(CancellationToken cancellationToken);
    Task<List<GetSubCategoryDto>> GetAll(CancellationToken cancellationToken);
}
