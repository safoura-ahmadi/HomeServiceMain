namespace HomeService.Domain.Core.Contracts.Service.Users;

public interface IAdminService
{
    Task<bool> CharcheBalnce(decimal money, CancellationToken cancellationToken);
}
