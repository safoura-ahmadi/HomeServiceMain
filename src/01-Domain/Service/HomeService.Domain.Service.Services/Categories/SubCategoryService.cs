using HomeService.Domain.Core.Contracts.Repository.Categories;
using HomeService.Domain.Core.Contracts.Service.Categories;
using HomeService.Domain.Core.Dtos.Categories;

namespace HomeService.Domain.Service.Services.Categories;

public class SubCategoryService(ISubCategoryRepository repository) : ISubCategoryService
{
    private readonly ISubCategoryRepository _repository = repository;

    public async Task<bool> Create(string title, int CategoryId, CancellationToken cancellationToken)
    {
        return await _repository.Create(title, CategoryId, cancellationToken);
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        return await _repository.Delete(id, cancellationToken);
    }

    public async Task<List<GetSubCategoryDto>> GetAll(CancellationToken cancellationToken)
    {
        return await _repository.GetAll(cancellationToken);
    }

    public async Task<List<GetSubCategoryDto>> GetByCategoryId(int categoryId, CancellationToken cancellationToken)
    {
        return await _repository.GetByCategoryId(categoryId, cancellationToken);
    }

    public async Task<bool> Update(UpdateSubCategoryDto model, CancellationToken cancellationToken)
    {
        return await _repository.Update(model, cancellationToken);
    }
}
