namespace HomeService.Domain.Core.Contracts.Service.Users;
public interface IExpertSubServiceService
{
    Task<bool> Create(int expertId, int subServiceId, CancellationToken cancellationToken);
    Task<Dictionary<int, string>> GetSubServicesForExpert(int expertId, CancellationToken cancellationToken);
}
