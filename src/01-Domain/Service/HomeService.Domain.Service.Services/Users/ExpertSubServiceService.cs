using HomeService.Domain.Core.Contracts.Repository.Users;
using HomeService.Domain.Core.Contracts.Service.Users;

namespace HomeService.Domain.Service.Services.Users;

public class ExpertSubServiceService(IExpertSubServiceRepository repository) : IExpertSubServiceService
{
    private readonly IExpertSubServiceRepository _repository;
    public async Task<bool> Create(int expertId, int subServiceId, CancellationToken cancellationToken)
    {
        return await _repository.Create(expertId, subServiceId, cancellationToken);
    }

    public async Task<Dictionary<int, string>> GetSubServicesForExpert(int expertId, CancellationToken cancellationToken)
    {
        return await _repository.GetSubServicesForExpert(expertId, cancellationToken);
    }
}
