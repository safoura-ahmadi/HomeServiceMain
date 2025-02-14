using HomeService.Domain.Core.Contracts.Repository.Categories;
using HomeService.Domain.Core.Contracts.Service.Categories;
using HomeService.Domain.Core.Dtos.Categories;

namespace HomeService.Domain.Service.Services.Categories;

public class CategoryService(ICategoryRepository repository) : ICategoryService

{
    private readonly ICategoryRepository _repository = repository;

    public async Task<bool> Create(string title, string imagePath, CancellationToken cancellationToken)
    {
        return await _repository.Create(title, imagePath, cancellationToken);
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        return await _repository.Delete(id, cancellationToken);
    }

    public async Task<List<GetCategoryForAdminPageDto>> GetAll(CancellationToken cancellationToken)
    {
        return await _repository.GetAll(cancellationToken);
    }

    public async Task<List<GetCategoryForMainPageDto>> GetAllForMainPage(CancellationToken cancellationToken)
    {
       return await _repository.GetAllForMainPage(cancellationToken);
    }

    public async Task<bool> Update(int id, string title, CancellationToken cancellationToken)
    {
        return await _repository.Update(id, title, cancellationToken);
    }
}
