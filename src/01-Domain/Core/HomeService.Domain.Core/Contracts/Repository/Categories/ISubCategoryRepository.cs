using HomeService.Domain.Core.Dtos.Categories;
using HomeService.Domain.Core.Entities;

namespace HomeService.Domain.Core.Contracts.Repository.Categories;

public interface ISubCategoryRepository
{
    Task<List<GetSubCategoryDto>> GetByCategoryId(int categoryId, CancellationToken cancellationToken);
    Task<List<GetSubCategoryDto>> GetAll(CancellationToken cancellationToken);
    Task<Result> Create(string title, int CategoryId, CancellationToken cancellationToken);
    Task<Result> Delete(int id, CancellationToken cancellationToken);
    Task<Result> Update(UpdateSubCategoryDto model, CancellationToken cancellationToken);
    Task<UpdateSubCategoryDto?> GetById(int id, CancellationToken cancellationToken);

}
