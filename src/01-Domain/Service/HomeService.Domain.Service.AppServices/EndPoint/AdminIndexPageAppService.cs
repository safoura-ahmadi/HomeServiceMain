using HomeService.Domain.Core.Contracts.AppService.EndPoint;
using HomeService.Domain.Core.Contracts.AppService.Users;
using HomeService.Domain.Core.Contracts.Service.Orders;
using HomeService.Domain.Core.Contracts.Service.Users;
using HomeService.Domain.Core.Dtos.EndPoint;
using HomeService.Domain.Core.Dtos.Orders;

namespace HomeService.Domain.Service.AppServices.EndPoint;

public class AdminIndexPageAppService(IExpertService expertService, ICustomerService customerService, IOrderService orderService, ISuggestionService suggestionService) : IAdminIndexPageAppService
{
    private readonly IExpertService _expertService = expertService;
    private readonly ICustomerService _customerService = customerService;
    private readonly IOrderService _orderService = orderService;
    private readonly ISuggestionService _suggestionService = suggestionService;

    public async Task<List<GetLastOrderDto>> GetLatestOrders(CancellationToken cancellationToken)
    {
        return await _orderService.GetLatestOrders(cancellationToken);
    }

    public async Task<List<GetlastSuggestionDto>> GetLatestSuggestions(CancellationToken cancellationToken)
    {
        return await _suggestionService.GetLatestSuggestions(cancellationToken);
    }

    public async Task<StatisticsDataDto> GetStatisticsData(CancellationToken cancellationToken)
    {
        var item = new StatisticsDataDto()
        {
            CustomerCount = await _customerService.GetTotalCount(cancellationToken),
            ExpertCount = await _expertService.GetTotalCount(cancellationToken),
            OrderCount = await _orderService.GetTotalConut(cancellationToken),
            SuggestionCount = await _suggestionService.GetTotalCount(cancellationToken)
        };
        return item;

    }
}
