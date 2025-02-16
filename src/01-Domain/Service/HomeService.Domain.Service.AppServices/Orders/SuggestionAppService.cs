using HomeService.Domain.Core.Contracts.AppService.Orders;
using HomeService.Domain.Core.Contracts.Service.Orders;
using HomeService.Domain.Core.Dtos.Orders;

namespace HomeService.Domain.Service.AppServices.Orders;

public class SuggestionAppService(ISuggestionService suggestionService) : ISuggestionAppSrvice

{
    private readonly ISuggestionService _suggestionService = suggestionService;

    public Task<List<SuggestionDto>> GetByOrderId(int orderId, CancellationToken cancellationToken)
    {
       return _suggestionService.GetByOrderId(orderId,cancellationToken);
    }
}
