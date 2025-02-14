using HomeService.Domain.Core.Contracts.Repository.Users;
using HomeService.Domain.Core.Contracts.Service.Users;

namespace HomeService.Domain.Service.Services.Users;

public class AdminService(IAdminRepository repository ) : IAdminService
{
    private readonly IAdminRepository _repository = repository;

    public async Task<bool> CharcheBalnce(decimal money, CancellationToken cancellationToken)
    {
        return await _repository.CharcheBalnce(money, cancellationToken);
    }
}
