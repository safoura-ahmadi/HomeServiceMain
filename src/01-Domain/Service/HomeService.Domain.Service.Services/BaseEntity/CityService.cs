using HomeService.Domain.Core.Contracts.Repository.BaseEntities;
using HomeService.Domain.Core.Contracts.Service.BaseEntities;
using HomeService.Domain.Core.Entities.BaseEntities;

namespace HomeService.Domain.Service.Services.BaseEntity;

public class CityService(ICityRepository repository) : ICityService
{
    private readonly ICityRepository _repository = repository;
    public async Task<List<City>> GetAll(CancellationToken cancellationToken)
    {
       return await _repository.GetAll(cancellationToken);
    }
}
