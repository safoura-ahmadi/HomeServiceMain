using HomeService.Domain.Core.Contracts.Repository.BaseEntities;
using HomeService.Domain.Core.Contracts.Service.BaseEntities;
using HomeService.Domain.Core.Dtos.Categories;
using HomeService.Domain.Core.Entities.BaseEntities;
using Microsoft.Extensions.Caching.Memory;

namespace HomeService.Domain.Service.Services.BaseEntity;

public class CityService(ICityRepository repository,IMemoryCache memoryCache) : ICityService
{
    private readonly ICityRepository _repository = repository;
    public async Task<List<City>> GetAll(CancellationToken cancellationToken)
    {
        List<City> item = memoryCache.Get<List<City>>("CityList") ?? [];

        if (item.Count > 0)
        {

        }
        else
        {
            item = await _repository.GetAll(cancellationToken);
            memoryCache.Set("CityList", item,
                new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromDays(7)
                }
                );
        }


   
        return item;
       
    }
}
