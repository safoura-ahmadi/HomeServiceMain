using HomeService.Domain.Core.Contracts.Repository.Categories;
using HomeService.Domain.Core.Contracts.Service.Categories;
using HomeService.Domain.Core.Dtos.Categories;
using HomeService.Domain.Core.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace HomeService.Domain.Service.Services.Categories;

public class SubCategoryService(ISubCategoryRepository repository, IMemoryCache memoryCache) : ISubCategoryService
{
    private readonly ISubCategoryRepository _repository = repository;
    private readonly IMemoryCache _memoryCache = memoryCache;

    public async Task<Result> Create(string title, int CategoryId, CancellationToken cancellationToken)
    {
        return await _repository.Create(title, CategoryId, cancellationToken);
    }

    public async Task<Result> Delete(int id, CancellationToken cancellationToken)
    {
        return await _repository.Delete(id, cancellationToken);
    }

    public async Task<List<GetSubCategoryDto>> GetAll(CancellationToken cancellationToken)
    {
        List<GetSubCategoryDto> item = _memoryCache.Get<List<GetSubCategoryDto>>("AllSubCategoryList") ?? [];

        if (item.Count > 0)
        {

        }
        else
        {
            item = await _repository.GetAll(cancellationToken);
            _memoryCache.Set("AllSubCategoryList", item, TimeSpan.FromHours(12));
        }
        return item;

    }

    public async Task<List<GetSubCategoryDto>> GetByCategoryId(int categoryId, CancellationToken cancellationToken)
    {
        List<GetSubCategoryDto> item = _memoryCache.Get<List<GetSubCategoryDto>>("CategorySubCategoryList") ?? [];

        if (item.Count > 0)
        {
           
        }
        else
        {
            item = await _repository.GetByCategoryId(categoryId, cancellationToken);
            _memoryCache.Set("CategorySubCategoryList", item, TimeSpan.FromHours(12));
        }
        return item;

    }

    public async Task<UpdateSubCategoryDto?> GetById(int id, CancellationToken cancellationToken)
    {
        return await _repository.GetById(id, cancellationToken);
    }

    public async Task<Result> Update(UpdateSubCategoryDto model, CancellationToken cancellationToken)
    {
        return await _repository.Update(model, cancellationToken);
    }
}
