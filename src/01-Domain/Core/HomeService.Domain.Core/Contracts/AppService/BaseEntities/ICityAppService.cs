using HomeService.Domain.Core.Entities.BaseEntities;

namespace HomeService.Domain.Core.Contracts.AppService.BaseEntities;

public interface ICityAppService
{
    Task<List<City>> GetAll(CancellationToken cancellationToken);
}
