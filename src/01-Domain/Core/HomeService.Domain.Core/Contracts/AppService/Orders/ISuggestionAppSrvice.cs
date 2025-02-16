using HomeService.Domain.Core.Dtos.Orders;

namespace HomeService.Domain.Core.Contracts.AppService.Orders;

public interface ISuggestionAppSrvice
{
    Task<List<SuggestionDto>> GetByOrderId(int orderId, CancellationToken cancellationToken);

}
