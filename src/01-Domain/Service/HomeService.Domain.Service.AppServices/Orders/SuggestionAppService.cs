using HomeService.Domain.Core.Contracts.AppService.Orders;
using HomeService.Domain.Core.Contracts.Service.Orders;
using HomeService.Domain.Core.Dtos.Orders;

namespace HomeService.Domain.Service.AppServices.Orders;

public class SuggestionAppService(ISuggestionService suggestionService) : ISuggestionAppService

{
    private readonly ISuggestionService _suggestionService = suggestionService;

    public Task<List<GetSuggestionForOrderDto>> GetByOrderId(int orderId, CancellationToken cancellationToken)
    {
       return _suggestionService.GetByOrderId(orderId,cancellationToken);
    }

    public async Task<List<GetlastSuggestionDto>> GetLatestSuggestions(CancellationToken cancellationToken)
    {
        return await _suggestionService.GetLatestSuggestions(cancellationToken);
    }
}
