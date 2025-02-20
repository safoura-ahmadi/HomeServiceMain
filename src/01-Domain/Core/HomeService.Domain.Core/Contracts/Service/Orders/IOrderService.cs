using HomeService.Domain.Core.Dtos.Orders;
using HomeService.Domain.Core.Entities;
using HomeService.Domain.Core.Enums.Orders;

namespace HomeService.Domain.Core.Contracts.Service.Orders;

public interface IOrderService
{
    Task<int> Create(CreateOrderDto order, CancellationToken cancellationToken);
    Task<List<GetOrderDto>> GetAvailableOrdersForExpert(int cityId, int subserviceId, CancellationToken cancellationToken);

    Task<Result> ChangeStateToWaitingForExpertOffer(int id, CancellationToken cancellationToken);
    Task<Result> ChangeStateToWaitingForExpertSelection(int id, CancellationToken cancellationToken);
    Task<Result> ChangeStateToWorkCompletedAndPaid(int id, CancellationToken cancellationToken);
    Task<Result> ChangeStateToExpertArrivedAtLocation(int id, CancellationToken cancellationToken);
    Task<OrderStatusEnum> GetLastStatusOfOrder(int id, CancellationToken cancellationToken);
    Task<Result> SetFinalPrice(int id, int price, CancellationToken cancellationToken);
    Task<Result> SetFinalTimeToDone(int id, DateTime timeToDone, CancellationToken cancellationToken);
    Task<List<GetAllOrderDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<int> GetTotalConut(CancellationToken cancellationToken);
    Task<Result> Delete(int id, CancellationToken cancellationToken);
    Task<List<GetOrderDto>> Search(string text, CancellationToken cancellationToken);
    //
    Task<List<GetLastOrderDto>> GetLatestOrders(CancellationToken cancellationToken);
}
