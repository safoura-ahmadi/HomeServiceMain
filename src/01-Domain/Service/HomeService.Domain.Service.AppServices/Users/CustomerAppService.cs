using HomeService.Domain.Core.Contracts.AppService.Users;
using HomeService.Domain.Core.Contracts.Service.Users;
using HomeService.Domain.Core.Entities;

namespace HomeService.Domain.Service.AppServices.Users;

public class CustomerAppService(ICustomerService customerService) : ICustomerAppService
{
    private readonly ICustomerService _customerService = customerService;

    public async Task<Result> Create(int userId, string lName, int cityId, CancellationToken cancellationToken)
    {
        if (userId <= 0 || cityId <= 0 || string.IsNullOrEmpty(lName))
            return Result.Fail("مشخصات وارد شده نامعتبر است");
        return await _customerService.Create(userId,lName,cityId,cancellationToken);
            
    }

    public async Task<int> GetTotalCount(CancellationToken cancellationToken)
    {
        return await _customerService.GetTotalCount(cancellationToken);
    }
}
