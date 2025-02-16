using HomeService.Domain.Core.Contracts.Repository.Categories;
using HomeService.Domain.Core.Contracts.Service.Categories;
using HomeService.Domain.Core.Dtos.Categories;
using HomeService.Domain.Core.Entities;

namespace HomeService.Domain.Service.Services.Categories;

public class CategoryService(ICategoryRepository repository) : ICategoryService

{
    private readonly ICategoryRepository _repository = repository;

    public async Task<Result> Create(string title, string imagePath, CancellationToken cancellationToken)
    {
        return await _repository.Create(title, imagePath, cancellationToken);
    }

    public async Task<Result> Delete(int id, CancellationToken cancellationToken)
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

    public async Task<Result> Update(int id, string title, CancellationToken cancellationToken)
    {
        return await _repository.Update(id, title, cancellationToken);
    }
}
