using HomeService.Domain.Core.Dtos.Categories;

namespace HomeService.Domain.Core.Contracts.Repository.Categories;

public interface ISubCategoryDapperRepo
{
    Task<List<GetSubCategoryDto>> GetAll(CancellationToken cancellationToken);
}
