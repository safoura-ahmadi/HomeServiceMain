using HomeService.Domain.Core.Entities;

namespace HomeService.Domain.Core.Contracts.AppService.Users;

public interface IExpertAppService
{
    Task<Result> Create(int userId, string lName, CancellationToken cancellationToken);
    Task<int> GetTotalCount(CancellationToken cancellationToken);
}
