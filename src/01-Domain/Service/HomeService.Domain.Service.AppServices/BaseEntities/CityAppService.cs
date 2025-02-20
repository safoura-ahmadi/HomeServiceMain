using HomeService.Domain.Core.Contracts.AppService.BaseEntities;
using HomeService.Domain.Core.Contracts.Service.BaseEntities;
using HomeService.Domain.Core.Entities.BaseEntities;

namespace HomeService.Domain.Service.AppServices.BaseEntities;

public class CityAppService(ICityService cityService) : ICityAppService
{
    private readonly ICityService _cityService = cityService;

    public async Task<List<City>> GetAll(CancellationToken cancellationToken)
    {
        return await _cityService.GetAll(cancellationToken);
    }
}
