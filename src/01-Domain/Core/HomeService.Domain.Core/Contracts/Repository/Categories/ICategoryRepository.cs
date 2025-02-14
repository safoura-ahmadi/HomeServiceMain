using HomeService.Domain.Core.Dtos.Categories;

namespace HomeService.Domain.Core.Contracts.Repository.Categories;

public interface ICategoryRepository
{
    Task<List<GetCategoryForMainPageDto>> GetAllForMainPage(CancellationToken cancellationToken);
    Task<bool> Delete(int id, CancellationToken cancellationToken);
    Task<bool> Update(int id, string title, CancellationToken cancellationToken);
    Task<List<GetCategoryForAdminPageDto>> GetAll(CancellationToken cancellationToken);
    Task<bool> Create(string title, string imagePath, CancellationToken cancellationToken);

}
