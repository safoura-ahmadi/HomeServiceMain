using HomeService.Domain.Core.Dtos.Users;

namespace HomeService.Domain.Core.Contracts.AppService.Users;
public interface IExpertSubServiceAppservice
{
    Task<bool> Create(int expertId, List<int> subServiceIds, CancellationToken cancellationToken);
    Task<List<int>> GetSubServicesByExpertId(int expertId, CancellationToken cancellationToken);
}
