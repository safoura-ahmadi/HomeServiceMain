using HomeService.Domain.Core.Dtos.Users;
using HomeService.Domain.Core.Entities;

namespace HomeService.Domain.Core.Contracts.Service.Users;

public interface ICustomerService
{
    Task<int> GetTotalCount(CancellationToken cancellationToken);
    Task<Result> Create(int userId, string lName, int cityId, CancellationToken cancellationToken);
}
