using HomeService.Domain.Core.Contracts.AppService.Orders;
using HomeService.Domain.Core.Contracts.Service.Orders;
using HomeService.Domain.Core.Dtos.Orders;
using HomeService.Domain.Core.Entities;
using System.Diagnostics.CodeAnalysis;

namespace HomeService.Domain.Service.AppServices.Orders;

public class OrderAppService(IOrderService orderService, ISuggestionService suggestionService) : IOrderAppService
{
    private readonly IOrderService _orderService = orderService;
    private readonly ISuggestionService _suggestionService = suggestionService;

    public async Task<Result> ChangeStateToWaitingForExpertOffer(int id, CancellationToken cancellationToken)
    {
        var condition = await _suggestionService.IsOrderHaveActiveSuggestion(id, cancellationToken);
        if (condition.Success)
            return Result.Fail("متخصصان برای این سفارش پیشنهاد هایی ارایه کرده اند. قابلیت  تغییر وضعیت وجود ندارد");
        var result = await _orderService.ChangeStateToWaitingForExpertOffer(id, cancellationToken);
        return result;
    
    }
    public async Task<Result> ChangeStateToWaitingForExpertSelection(int id,CancellationToken cancellationToken)
    {
        var condtion1 = await _suggestionService.IsOrderHaveActiveSuggestion(id, cancellationToken);
        if (!condtion1.Success)
            return Result.Fail("پیشنهادی از سمت متخصصان برای این سفارش وجود ندارد.تغییر وضعیت مجاز نیست");
        var condition2 = await _suggestionService.IsOrderHaveAcceptedSugestion(id, cancellationToken);
        if (condition2.Success)
            return Result.Fail("برای این سفارش، پیشنهادی از قبل تایید شده. تغییر وضعیت امکانپذیر نیست");
        return await _orderService.ChangeStateToWaitingForExpertSelection(id,cancellationToken);
    }
    public async Task<Result> ChangeStateToExpertArrivedAtLocation(int id, CancellationToken cancellationToken )
    {
        var condion1 = await _suggestionService.IsOrderHaveActiveSuggestion(id, cancellationToken);
        if (!condion1.Success)
            return Result.Fail("پیشنهادی  از سمت متخصصان برای این سفارش وجود ندارد.تغییر وضعیت مجاز نیست");
        var condition2 = await _suggestionService.IsOrderHaveAcceptedSugestion(id, cancellationToken);
        if (!condition2.Success)
            return Result.Fail("پیشنهادی برای این سفارش تایید نشده است. تغییر وضعیت مجاز نیست");
        var condition3 = await _orderService.GetLastStatusOfOrder(id, cancellationToken) != Core.Enums.Orders.OrderStatusEnum.WorkCompletedAndPaid? true : false;
        if (!condition3)
            return Result.Fail("این سفارش در مرحله ی پرداخت قرار دارد، تغییر وضعیت مجاز نیست");
        return await _orderService.ChangeStateToExpertArrivedAtLocation(id,cancellationToken);


    }
    public async Task<Result> ChangeStateToWorkCompletedAndPaid(int id,CancellationToken cancellationToken)
    {
        var condion1 = await _suggestionService.IsOrderHaveActiveSuggestion(id, cancellationToken);
        if (!condion1.Success)
            return Result.Fail("پیشنهادی  از سمت متخصصان برای این سفارش وجود ندارد.تغییر وضعیت مجاز نیست");
        var condition2 = await _suggestionService.IsOrderHaveAcceptedSugestion(id, cancellationToken);
        if (!condition2.Success)
            return Result.Fail("پیشنهادی برای این سفارش تایید نشده است. تغییر وضعیت مجاز نیست");
        return await _orderService.ChangeStateToWorkCompletedAndPaid(id, cancellationToken);
    }

    public async Task<List<GetOrderDto>> GetAll(int pageNumber,CancellationToken cancellationToken)
    {
        if (pageNumber <= 0)
            pageNumber = 1;
        return await _orderService.GetAll(pageNumber, 10, cancellationToken);
    }

    public async Task<int> GetTotalConut(CancellationToken cancellationToken)
    {
        return await _orderService.GetTotalConut(cancellationToken);
    }

    public async Task<List<GetOrderDto>> Search(string text, CancellationToken cancellationToken)
    {
        return await _orderService.Search(text, cancellationToken);
    }
}

