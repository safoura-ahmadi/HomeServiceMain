using HomeService.Domain.Core.Contracts.AppService.Users;
using HomeService.Domain.Core.Contracts.Service.Users;
using HomeService.Domain.Core.Dtos.Users;
using HomeService.Domain.Core.Entities.Users;
using System.Runtime.CompilerServices;

namespace HomeService.Domain.Service.AppServices.Users;

public class ExpertSubServiceAppService(IExpertSubServiceService expertSubServiceService) : IExpertSubServiceAppservice
{
    public async Task<bool> Create(int expertId, List<int> subServiceIds, CancellationToken cancellationToken)
    {
        if (expertId <= 0  || subServiceIds.Any(x => x <= 0))
            return false;
        await expertSubServiceService.Delete(expertId, cancellationToken);
        return await expertSubServiceService.Create(expertId, subServiceIds, cancellationToken);
    }

    public async Task<List<int>> GetSubServicesByExpertId(int expertId, CancellationToken cancellationToken)
    {
        if (expertId <= 0)
            return [];
        return await expertSubServiceService.GetSubServicesByExpertId(expertId, cancellationToken);
    }
}
