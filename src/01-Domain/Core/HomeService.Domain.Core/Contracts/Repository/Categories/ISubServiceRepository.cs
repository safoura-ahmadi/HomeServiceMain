using HomeService.Domain.Core.Dtos.Categories;
using HomeService.Domain.Core.Entities;

namespace HomeService.Domain.Core.Contracts.Repository.Categories;

public interface ISubServiceRepository
{
    Task<Result> Create(CreateSubServiceDto model, CancellationToken cancellationToken);
    Task<List<GetSubServiceDto>> GetBySubCategoryId(int subcategoryId, CancellationToken cancellationToken);
    Task<List<GetSubServiceDto>> Search(string text, CancellationToken cancellationToken);
    Task<int> GetBasePrice(int id, CancellationToken cancellationToken);
    Task<Result> Update(UpdateSubServiceDto model, CancellationToken cancellationToken);
    Task<Result> Delete(int id, CancellationToken cancellationToken);
    Task<List<GetSubServiceDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<int> GetTotalCount(CancellationToken cancellationToken);
    Task<UpdateSubServiceDto?> GetById(int id, CancellationToken cancellationToken);

}
