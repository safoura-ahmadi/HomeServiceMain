using HomeService.Domain.Core.Contracts.Repository.Categories;
using HomeService.Domain.Core.Contracts.Service.Categories;
using HomeService.Domain.Core.Dtos.Categories;

namespace HomeService.Domain.Service.Services.Categories;

public class SubServiceService(ISubServiceRepository repository) : ISubServiceService
{
    private readonly ISubServiceRepository _repository = repository;

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        return await _repository.Delete(id, cancellationToken);
    }

    public async Task<List<GetSubServiceDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
       return await _repository.GetAll(pageNumber, pageSize, cancellationToken);
    }

    public async Task<int> GetBasePrice(int id, CancellationToken cancellationToken)
    {
        return await _repository.GetBasePrice(id, cancellationToken);
    }

    public async Task<List<GetSubServiceDto>> GetBySubCategoryId(int subcategoryId, CancellationToken cancellationToken)
    {
        return await _repository.GetBySubCategoryId(subcategoryId, cancellationToken);
    }

    public async Task<int> GetTotalCount(CancellationToken cancellationToken)
    {
        return await _repository.GetTotalCount(cancellationToken);
    }

    public async Task<List<GetSubServiceDto>> Search(string text, CancellationToken cancellationToken)
    {
        return await _repository.Search(text, cancellationToken);
    }

    public async Task<bool> Update(GetSubServiceDto model, CancellationToken cancellationToken)
    {
        return await _repository.Update(model, cancellationToken);
    }
}
