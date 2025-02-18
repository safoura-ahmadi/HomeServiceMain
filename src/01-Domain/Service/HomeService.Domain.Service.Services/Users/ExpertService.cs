using HomeService.Domain.Core.Contracts.Repository.Users;
using HomeService.Domain.Core.Contracts.Service.Users;
using HomeService.Domain.Core.Entities;

namespace HomeService.Domain.Service.Services.Users;

public class ExpertService(IExpertRepository repository) : IExpertService
{
    private readonly IExpertRepository _repository = repository;

    public async Task<Result> Create(int userId, string lName, CancellationToken cancellationToken)
    {
        return await _repository.Create(userId, lName, cancellationToken);
    }

    public async Task<int> GetTotalCount(CancellationToken cancellationToken)
    {
        return await _repository.GetTotalCount(cancellationToken);
    }


}
