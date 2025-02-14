namespace HomeService.Domain.Core.Contracts.Repository.Users;

public interface IAdminRepository
{
    Task<bool> CharcheBalnce(decimal money, CancellationToken cancellationToken);
}
