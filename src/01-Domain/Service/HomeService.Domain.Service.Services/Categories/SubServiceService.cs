using HomeService.Domain.Core.Contracts.Repository.Categories;
using HomeService.Domain.Core.Contracts.Service.Categories;
using HomeService.Domain.Core.Dtos.Categories;
using HomeService.Domain.Core.Entities;
using HomeService.Domain.Core.Entities.Categories;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Caching.Memory;

namespace HomeService.Domain.Service.Services.Categories;

public class SubServiceService(ISubServiceRepository repository,ISubServiceDapperRepo dapperRepo, IMemoryCache memoryCache) : ISubServiceService
{
    private readonly ISubServiceRepository _repository = repository;
    private readonly IMemoryCache _memoryCache = memoryCache;

    public async Task<Result> Create(CreateSubServiceDto model, CancellationToken cancellationToken)
    {

        return await _repository.Create(model, cancellationToken);
    }

    public async Task<Result> Delete(int id, CancellationToken cancellationToken)
    {
        return await _repository.Delete(id, cancellationToken);
    }

    public async Task<List<GetSubServiceDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {

        List<GetSubServiceDto> item = _memoryCache.Get<List<GetSubServiceDto>>($"SubService{pageNumber}-{pageSize}List") ?? [];

        if (item.Count > 0)
        {

        }
        else
        {
            item = await dapperRepo.GetAll(cancellationToken);
            item = item
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            _memoryCache.Set($"SubService{pageNumber}-{pageSize}List", item, TimeSpan.FromHours(12));
        }
        return item;
       
    }

    public async Task<List<GetExpertPageSubServiceDto>> GetAllForExpertPages(CancellationToken cancellationToken)
    {
        return await _repository.GetAllForExpertPages(cancellationToken);
    }

    public async Task<int> GetBasePrice(int id, CancellationToken cancellationToken)
    {
        return await _repository.GetBasePrice(id, cancellationToken);
    }

    public async Task<UpdateSubServiceDto?> GetById(int id, CancellationToken cancellationToken)
    {

        return await _repository.GetById(id, cancellationToken);
    }

    public async Task<List<GetSubServiceDto>> GetBySubCategoryId(int subcategoryId, CancellationToken cancellationToken)
    {
        List<GetSubServiceDto> item = _memoryCache.Get<List<GetSubServiceDto>>($"SubCategory{subcategoryId}ServiceList") ?? [];

        if (item.Count > 0)
        {

        }
        else
        {
            item = await _repository.GetBySubCategoryId(subcategoryId, cancellationToken);
            _memoryCache.Set($"SubCategory{subcategoryId}ServiceList", item, TimeSpan.FromHours(12));
        }
        return item;

    }

    public async Task<int> GetTotalCount(CancellationToken cancellationToken)
    {
        return await _repository.GetTotalCount(cancellationToken);
    }

    public async Task<List<GetSubServiceDto>> Search(string text, CancellationToken cancellationToken)
    {
        return await _repository.Search(text, cancellationToken);
    }

    public async Task<Result> Update(UpdateSubServiceDto model, CancellationToken cancellationToken)
    {
        return await _repository.Update(model, cancellationToken);
    }
}
