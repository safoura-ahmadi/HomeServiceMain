using HomeService.Domain.Core.Contracts.Repository.Categories;
using HomeService.Domain.Core.Contracts.Service.Categories;
using HomeService.Domain.Core.Dtos.Categories;
using HomeService.Domain.Core.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace HomeService.Domain.Service.Services.Categories;

public class CategoryService(ICategoryRepository repository,ICategoryDapperRepo dapperRepo, IMemoryCache memoryCache) : ICategoryService

{
    private readonly ICategoryRepository _repository = repository;
    private readonly IMemoryCache _memoryCache = memoryCache;

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
        List<GetCategoryForAdminPageDto> item = _memoryCache.Get<List<GetCategoryForAdminPageDto>>("AdminCategoryList") ?? [];

        if ( item.Count > 0)
        {
           
        }
        else
        {
            item = await dapperRepo.GetAll(cancellationToken);
            _memoryCache.Set("AdminCategoryList", item, TimeSpan.FromHours(12));
        }


        //_memoryCache.Set("ProductList", products, 
        //    new MemoryCacheEntryOptions
        //    {
        //        SlidingExpiration = TimeSpan.FromSeconds(5)
        //    });
        return item;
    }

    public async Task<List<GetCategoryForMainPageDto>> GetAllForMainPage(CancellationToken cancellationToken)
    {
        List<GetCategoryForMainPageDto> item = _memoryCache.Get<List<GetCategoryForMainPageDto>>("MainPageCategoryList") ?? [];

        if (item.Count > 0)
        {
           
        }
        else
        {
            item = await _repository.GetAllForMainPage(cancellationToken);
            _memoryCache.Set("MainPageCategoryList", item, TimeSpan.FromHours(12));
        }
        return item;


    }

    public async Task<UpdateCategoryDto?> GetById(int id, CancellationToken cancellationToken)
    {
        return await _repository.GetById(id, cancellationToken);
    }

    public async Task<Result> Update(int id, string title, string imagePath, CancellationToken cancellationToken)
    {
        return await _repository.Update(id, title, imagePath, cancellationToken);
    }

    public async Task<Result> Update(UpdateCategoryDto model, CancellationToken cancellationToken)
    {

        return await _repository.Update(model, cancellationToken);

    }
}
