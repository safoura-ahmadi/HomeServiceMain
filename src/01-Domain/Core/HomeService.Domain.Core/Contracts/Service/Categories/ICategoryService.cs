using HomeService.Domain.Core.Dtos.Categories;
using HomeService.Domain.Core.Entities;

namespace HomeService.Domain.Core.Contracts.Service.Categories;

public interface ICategoryService
{
    Task<List<GetCategoryForMainPageDto>> GetAllForMainPage(CancellationToken cancellationToken);
    Task<Result> Delete(int id, CancellationToken cancellationToken);
    Task<Result> Update(int id, string title, CancellationToken cancellationToken);
    Task<List<GetCategoryForAdminPageDto>> GetAll(CancellationToken cancellationToken);
    Task<Result> Create(string title, string imagePath, CancellationToken cancellationToken);

}
