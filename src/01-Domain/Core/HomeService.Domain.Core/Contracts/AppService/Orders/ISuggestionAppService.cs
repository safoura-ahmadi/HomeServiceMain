using HomeService.Domain.Core.Dtos.Orders;

namespace HomeService.Domain.Core.Contracts.AppService.Orders;

public interface ISuggestionAppService
{
    Task<List<GetSuggestionForOrderDto>> GetByOrderId(int orderId, CancellationToken cancellationToken);
    //
    Task<List<GetlastSuggestionDto>> GetLatestSuggestions(CancellationToken cancellationToken);
}
