using HomeService.Domain.Core.Dtos.Categories;

namespace HomeService.Domain.Core.Contracts.Repository.Categories;

public interface ICategoryDapperRepo
{
    Task<List<GetCategoryForAdminPageDto>> GetAll(CancellationToken cancellationToken);

}
