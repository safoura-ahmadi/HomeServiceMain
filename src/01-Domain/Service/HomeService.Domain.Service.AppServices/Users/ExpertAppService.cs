using HomeService.Domain.Core.Contracts.AppService.Users;
using HomeService.Domain.Core.Contracts.Service.Users;
using HomeService.Domain.Core.Entities;

namespace HomeService.Domain.Service.AppServices.Users;

public class ExpertAppService(IExpertService expertService) : IExpertAppService
{
    private readonly IExpertService _expertService = expertService;

    public async Task<Result> Create(int userId, string lName, CancellationToken cancellationToken)
    {
        if (userId <= 0 || string.IsNullOrEmpty(lName))
            return Result.Fail("مشخصات وارد شده نامعتبر است");
        return await _expertService.Create(userId, lName, cancellationToken);
    }

    public async Task<int> GetTotalCount(CancellationToken cancellationToken)
    {
        return await _expertService.GetTotalCount(cancellationToken);
    }
}
