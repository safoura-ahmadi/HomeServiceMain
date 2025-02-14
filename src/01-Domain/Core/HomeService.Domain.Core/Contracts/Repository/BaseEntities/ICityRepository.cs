using HomeService.Domain.Core.Entities.BaseEntities;

namespace HomeService.Domain.Core.Contracts.Repository.BaseEntities;

public interface ICityRepository
{
    Task<List<City>> GetAll(CancellationToken cancellationToken);
}
