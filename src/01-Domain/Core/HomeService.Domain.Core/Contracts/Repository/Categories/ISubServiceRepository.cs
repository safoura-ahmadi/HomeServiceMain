using HomeService.Domain.Core.Dtos.Categories;

namespace HomeService.Domain.Core.Contracts.Repository.Categories;

public interface ISubServiceRepository
{
    Task<List<GetSubServiceDto>> GetBySubCategoryId(int subcategoryId, CancellationToken cancellationToken);
    Task<List<GetSubServiceDto>> Search(string text, CancellationToken cancellationToken);
    Task<int> GetBasePrice(int id, CancellationToken cancellationToken);
    Task<bool> Update(GetSubServiceDto model, CancellationToken cancellationToken);
    Task<bool> Delete(int id, CancellationToken cancellationToken);
    Task<List<GetSubServiceDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<int> GetTotalCount(CancellationToken cancellationToken);

}
