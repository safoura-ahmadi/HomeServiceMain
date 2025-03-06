using HomeService.Domain.Core.Contracts.AppService.Orders;
using HomeService.Domain.Core.Contracts.Service.BaseEntities;
using HomeService.Domain.Core.Contracts.Service.Orders;
using HomeService.Domain.Core.Dtos.Orders;
using HomeService.Domain.Core.Entities;

namespace HomeService.Domain.Service.AppServices.Orders;

public class SuggestionAppService(ISuggestionService suggestionService, ICommentService commentService, IOrderService orderService) : ISuggestionAppService

{
    private readonly ISuggestionService _suggestionService = suggestionService;
    private readonly ICommentService _commentService = commentService;
    private readonly IOrderService _orderService = orderService;

    public async Task<int> GetCustomerActiveSuggestionsCount(int customerId, CancellationToken cancellationToken)
    {
        if (customerId <= 0)
            return 0;
        return await _suggestionService.GetCustomerActiveSuggestionsCount(customerId, cancellationToken);
    }

    public async Task<int> GetExpertActiveSuggestionsCount(int expertId, CancellationToken cancellationToken)
    {
        if (expertId <= 0)
            return 0;
        return await _suggestionService.GetExpertActiveSuggestionsCount(expertId, cancellationToken);
    }

    public Task<List<GetSuggestionForOrderDto>> GetByOrderId(int orderId, CancellationToken cancellationToken)
    {
        return _suggestionService.GetByOrderId(orderId, cancellationToken);
    }

    public async Task<SuggestionDetailsDto?> GetDetailById(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return null;
        var item = await _suggestionService.GetDetailById(id, cancellationToken);
        if (item is not null)
            item.Score = await _commentService.GetExpertScore(item.ExpertId, cancellationToken);
        return item;
    }

    public async Task<List<GetlastSuggestionDto>> GetLatestSuggestions(CancellationToken cancellationToken)
    {
        return await _suggestionService.GetLatestSuggestions(cancellationToken);
    }
    public async Task<SuggestionDetailsDto?> GetSuggestionDetailById(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return null;
        var item = await _suggestionService.GetDetailById(id, cancellationToken);
        if (item is not null)
            item.Score = await _commentService.GetExpertScore(item.ExpertId, cancellationToken);
        return item;
    }

    public async Task<SuggestionOverviewDto?> GetById(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return null;
        return await _suggestionService.GetById(id, cancellationToken);
    }

    public async Task<Result> ChangeStatetoAccepted(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return Result.Fail("پیشنهادی با این مشخصات وجود ندارد");
        var suggest = await _suggestionService.GetById(id, cancellationToken);
        if (suggest is null)
            return Result.Fail("پیشنهادی با این مشخصات وجود ندارد");
        if (suggest.OrderId <= 0)
            return Result.Fail("برای این پیشنهاد سفارشی وجود ندارد");
        var orderState = await _orderService.GetLastStatusOfOrder(id, cancellationToken);
        if (orderState != Core.Enums.Orders.OrderStatusEnum.WaitingForExpertSelection)
            return Result.Fail("سفارش در وضعیت انتخاب متخصص قرار ندارد");
        var result = await _suggestionService.ChangeStatetoAccepted(id, cancellationToken);
        if (!result.Success)
            return result;
        var model = new UpdateOrderDto()
        {
            Id = suggest.OrderId,
            ExpertId = suggest.ExpertId,
            Price = suggest.Price,
            TimeToDone = suggest.TimeToDone,

        };
        return await _orderService.Update(model, cancellationToken);
    }
}
