using HomeService.Domain.Core.Dtos.Users;

namespace HomeService.Domain.Core.Contracts.Repository.Users;
public interface IExpertSubServiceRepository
{
    Task<bool> Create(int expertId, List<int> subServiceIds, CancellationToken cancellationToken);
    Task<List<int>> GetSubServicesByExpertId(int expertId, CancellationToken cancellationToken);
    Task<bool> Delete(int expertId, CancellationToken cancellationToken);
    Task<List<string>> GetExpertSkillsTitle(int expertId, CancellationToken cancellationToken);
}
