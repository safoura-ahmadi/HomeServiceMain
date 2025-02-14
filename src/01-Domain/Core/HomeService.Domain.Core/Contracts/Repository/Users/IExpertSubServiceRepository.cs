namespace HomeService.Domain.Core.Contracts.Repository.Users;
public interface IExpertSubServiceRepository
{
    Task<bool> Create(int expertId, int subServiceId, CancellationToken cancellationToken);
    Task<Dictionary<int, string>> GetSubServicesForExpert(int expertId, CancellationToken cancellationToken);
}
