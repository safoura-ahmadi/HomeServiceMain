using HomeService.Domain.Core.Dtos.EndPoint;
using HomeService.Domain.Core.Dtos.Orders;

namespace HomeService.Domain.Core.Contracts.AppService.EndPoint;

public interface IAdminIndexPageAppService
{
    Task<StatisticsDataDto> GetStatisticsData(CancellationToken cancellationToken);
    Task<List<GetlastSuggestionDto>> GetLatestSuggestions(CancellationToken cancellationToken);
    Task<List<GettOrderOverViewDto>> GetLatestOrders(CancellationToken cancellationToken);
    Task<SuggestionDetailsDto?> GetSuggestionDetailById(int id, CancellationToken cancellationToken);
}
