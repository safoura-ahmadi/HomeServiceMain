using HomeService.Domain.Core.Dtos.Categories;

namespace HomeService.Domain.Core.Contracts.AppService.Categories;

public interface ISubCategoryAppService
{
    Task<List<GetSubCategoryDto>> GetAll(CancellationToken cancellationToken);
}
