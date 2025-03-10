using HomeService.Domain.Core.Contracts.AppService.Categories;
using HomeService.Domain.Core.Contracts.AppService.Orders;
using HomeService.Domain.Core.Contracts.Service.BaseEntities;
using HomeService.Domain.Core.Contracts.Service.Categories;
using HomeService.Domain.Core.Contracts.Service.Orders;
using HomeService.Domain.Core.Dtos.Orders;
using HomeService.Domain.Core.Entities;
using HomeService.Domain.Core.Entities.Configs;
using HomeService.Domain.Core.Enums.Orders;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace HomeService.Domain.Service.AppServices.Orders;

public class OrderAppService(IOrderService orderService, ISuggestionService suggestionService, ILogger<OrderAppService> logger, IImageReposiotry imageService, ISubServiceService subService, SiteSetting siteSetting) : IOrderAppService
{
    private readonly IOrderService _orderService = orderService;
    private readonly ISuggestionService _suggestionService = suggestionService;
    private readonly ILogger<OrderAppService> _logger = logger;
    private readonly IImageReposiotry _imageService = imageService;
    private readonly ISubServiceService _subService = subService;
    private readonly SiteSetting _siteSetting = siteSetting;

    public async Task<Result> ChangeStateToWaitingForExpertOffer(int id, CancellationToken cancellationToken)
    {
        var condition = await _suggestionService.IsOrderHaveActiveSuggestion(id, cancellationToken);
        if (condition.Success)
            return Result.Fail("متخصصان برای این سفارش پیشنهاد هایی ارایه کرده اند. قابلیت  تغییر وضعیت وجود ندارد");
        var result = await _orderService.ChangeStateToWaitingForExpertOffer(id, cancellationToken);
        return result;

    }
    public async Task<Result> ChangeStateToWaitingForExpertSelection(int id, CancellationToken cancellationToken)
    {
        var condtion1 = await _suggestionService.IsOrderHaveActiveSuggestion(id, cancellationToken);
        if (!condtion1.Success)
            return Result.Fail("پیشنهادی از سمت متخصصان برای این سفارش وجود ندارد.تغییر وضعیت مجاز نیست");
        var condition2 = await _suggestionService.IsOrderHaveAcceptedSugestion(id, cancellationToken);
        if (condition2.Success)
            return Result.Fail("برای این سفارش، پیشنهادی از قبل تایید شده. تغییر وضعیت امکانپذیر نیست");
        return await _orderService.ChangeStateToWaitingForExpertSelection(id, cancellationToken);
    }

    public async Task<Result> ChangeStateToWorkCompletedAndPaid(int id, CancellationToken cancellationToken)
    {
        var condion1 = await _suggestionService.IsOrderHaveActiveSuggestion(id, cancellationToken);
        if (!condion1.Success)
            return Result.Fail("پیشنهادی  از سمت متخصصان برای این سفارش وجود ندارد.تغییر وضعیت مجاز نیست");
        var condition2 = await _suggestionService.IsOrderHaveAcceptedSugestion(id, cancellationToken);
        if (!condition2.Success)
            return Result.Fail("پیشنهادی برای این سفارش تایید نشده است. تغییر وضعیت مجاز نیست");
        return await _orderService.ChangeStateToWorkCompletedAndPaid(id, cancellationToken);
    }

    public async Task<List<GetAllOrderDto>> GetAll(int pageNumber, CancellationToken cancellationToken)
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
    //
    public async Task<List<GettOrderOverViewDto>> GetLatestOrders(CancellationToken cancellationToken)
    {
        return await _orderService.GetLatestOrders(cancellationToken);
    }

    public async Task<Result> Delete(int id, CancellationToken cancellationToken)
    {
        _logger.Log(LogLevel.Warning, "Attempt to delete{item}", "Order");
        if (id <= 0)
            return Result.Fail("سفارشی با این مشخصات وجود ندارد");
        return await _orderService.Delete(id, cancellationToken);
    }

    public async Task<GetOrderDto?> GetById(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return null;
        return await _orderService.GetById(id, cancellationToken);
    }

    public async Task<int> GetActiveOrdersCountByExpert(int expertId, CancellationToken cancellationToken)
    {
        if (expertId <= 0)
            return 0;
        return await _orderService.GetActiveOrdersCountByExpert(expertId, cancellationToken);
    }

    public async Task<int> GetActiveOrdersCountByCustomer(int customerId, CancellationToken cancellationToken)
    {
        return await _orderService.GetActiveOrdersCountByCustomer(customerId, cancellationToken);
    }

    public async Task<List<GettOrderOverViewDto>> GetCustomerOrders(int CustomerId, CancellationToken cancellationToken)
    {
        if (CustomerId <= 0)
            return [];
        return await _orderService.GetCustomerOrders(CustomerId, cancellationToken);
    }

    public async Task<Result> ChangeStateToCompleted(int id, CancellationToken cancellationToken)
    {
        var condion1 = await _suggestionService.IsOrderHaveActiveSuggestion(id, cancellationToken);
        if (!condion1.Success)
            return Result.Fail("پیشنهادی  از سمت متخصصان برای این سفارش وجود ندارد.تغییر وضعیت مجاز نیست");
        var condition2 = await _suggestionService.IsOrderHaveAcceptedSugestion(id, cancellationToken);
        if (!condition2.Success)
            return Result.Fail("پیشنهادی برای این سفارش تایید نشده است. تغییر وضعیت مجاز نیست");
        return await _orderService.ChangeStateToCompleted(id, cancellationToken);
    }

    public async Task<Result> Create(CreateOrderDto order, CancellationToken cancellationToken)
    {
        var imagesPath = new List<string>();
        if (order.SubServiceId <= 0)
            return Result.Fail("هوم سرویس این سفارش نامعتبر است");
        var serviceBasePrice = await _subService.GetBasePrice(order.SubServiceId, cancellationToken);
        if (order.Price < serviceBasePrice)
            return Result.Fail("قیمت پیشنهادی شما برای این سفارش نمیتواند کمتر از قیمت پایه ی هوم سرویس باشد");

        var orderId = await _orderService.Create(order, cancellationToken);
        if (orderId <= 0)
            return Result.Fail("در حین آپلود سفارش در دیتا بیس مشکلی ایجاد شده");
        if (order.Images is not null)
        {
            foreach (var image in order.Images)
            {
                var imagePath = await _imageService.UploadImage(image, "icon", cancellationToken);
                imagesPath.Add(imagePath);
            }

            var result = await _imageService.Create(imagesPath, orderId, cancellationToken);
            if (result.Success)
                return Result.Ok("سفارش شما با موفقیت ثبت شد");
            return Result.Fail($"سفارش شما با موفقیت ثبت شد، خطا :{result.Message}");
        }
        return Result.Ok("سفارش شما با موفقیت ثبت شد");
    }

    public async Task<Result> Update(UpdateOrderDto model, CancellationToken cancellationToken)
    {
        if (model.Id <= 0)
            return Result.Fail("سفارشی با این مشخصات وجود ندارد");
        return await _orderService.Update(model, cancellationToken);
    }

    public async Task<GetFinalOrderDto?> GetFinalInfoById(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return null;
        var item = await _orderService.GetFinalInfoById(id, cancellationToken);
        if (item is not null)
        {
            item.SiteFee = CalculateTotalFee(item.Price, _siteSetting.SiteFeePercent);
            item.TotalPrice = item.SiteFee + item.Price;
        }
        return item;
    }
    private decimal CalculateTotalFee(int price,int feePercent)
    {
        var fee = (decimal)(price * feePercent)/100;
        return fee;
    }

    public async Task<OrderStatusEnum> GetLastStatusOfOrder(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return OrderStatusEnum.UnDefined;
        return await _orderService.GetLastStatusOfOrder(id, cancellationToken);
    }

    public async Task<List<GetOrderDto>> GetAvailableOrdersForExpert(int expertId, int cityId, List< int> subserviceIds, CancellationToken cancellationToken)
    {
        if (expertId <= 0 || cityId <= 0 ||subserviceIds.Any(x => x <= 0))
            return [];
        return await _orderService.GetAvailableOrdersForExpert(expertId, cityId, subserviceIds, cancellationToken);

    }
}

