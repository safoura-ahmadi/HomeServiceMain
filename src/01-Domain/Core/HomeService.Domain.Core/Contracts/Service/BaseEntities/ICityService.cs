using HomeService.Domain.Core.Entities.BaseEntities;

namespace HomeService.Domain.Core.Contracts.Service.BaseEntities;

public interface ICityService
{
    Task<List<City>> GetAll(CancellationToken cancellationToken);
}
