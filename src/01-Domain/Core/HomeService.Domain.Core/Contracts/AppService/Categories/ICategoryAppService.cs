
using HomeService.Domain.Core.Dtos.Categories;
using HomeService.Domain.Core.Entities;

namespace HomeService.Domain.Core.Contracts.AppService.Categories;
public interface ICategoryAppService
{
    Task<Result> Delete(int id, CancellationToken cancellationToken);
    Task<Result> Update(int id, string title, CancellationToken cancellationToken);
    Task<List<GetCategoryForAdminPageDto>> GetAll(CancellationToken cancellationToken);
    Task<Result> Create(string title, string imagePath, CancellationToken cancellationToken);
}
