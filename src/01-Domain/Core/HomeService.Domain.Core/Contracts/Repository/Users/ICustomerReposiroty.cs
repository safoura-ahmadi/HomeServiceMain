using HomeService.Domain.Core.Dtos.Users;
using HomeService.Domain.Core.Entities;

namespace HomeService.Domain.Core.Contracts.Repository.Users;

public interface ICustomerRepository
{
    Task<Result> Create(int userId, string lName, int cityId, CancellationToken cancellationToken);
    Task<int> GetTotalCount(CancellationToken cancellationToken);
}
