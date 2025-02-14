using HomeService.Domain.Core.Dtos.Categories;

namespace HomeService.Domain.Core.Contracts.Repository.Categories;

public interface ISubCategoryRepository
{
    Task<List<GetSubCategoryDto>> GetByCategoryId(int categoryId, CancellationToken cancellationToken);
    Task<List<GetSubCategoryDto>> GetAll(CancellationToken cancellationToken);
    Task<bool> Create(string title, int CategoryId, CancellationToken cancellationToken);
    Task<bool> Delete(int id, CancellationToken cancellationToken);
    Task<bool> Update(UpdateSubCategoryDto model, CancellationToken cancellationToken);

}
