using HomeService.Domain.Core.Contracts.Repository.Users;
using HomeService.Domain.Core.Contracts.Service.Users;
using HomeService.Domain.Core.Dtos.Users;

namespace HomeService.Domain.Service.Services.Users;

public class ExpertSubServiceService(IExpertSubServiceRepository repository) : IExpertSubServiceService
{
    private readonly IExpertSubServiceRepository _repository = repository;
    public async Task<bool> Create(int expertId, List<int> subServiceIds, CancellationToken cancellationToken)
    {
        return await _repository.Create(expertId, subServiceIds, cancellationToken);
    }

    public async Task<bool> Delete(int expertId, CancellationToken cancellationToken)
    {
        if (expertId <= 0)
            return false;
        return await _repository.Delete(expertId, cancellationToken);
    }

    public async Task<List<int>> GetSubServicesByExpertId(int expertId, CancellationToken cancellationToken)
    {
        return await _repository.GetSubServicesByExpertId(expertId, cancellationToken);
    }


}
