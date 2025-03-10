using HomeService.Domain.Core.Entities;

namespace HomeService.Domain.Core.Contracts.AppService.Users;

public interface ICustomerAppService
{
    Task<int> GetTotalCount(CancellationToken cancellationToken);
    Task<Result> Create(int userId, CancellationToken cancellationToken);
}
