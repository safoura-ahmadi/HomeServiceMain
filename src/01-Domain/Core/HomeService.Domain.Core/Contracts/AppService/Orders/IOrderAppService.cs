using HomeService.Domain.Core.Dtos.Orders;
using HomeService.Domain.Core.Entities;
using HomeService.Domain.Core.Enums.Orders;


namespace HomeService.Domain.Core.Contracts.AppService.Orders;
public interface IOrderAppService
{
    Task<List<GetAllOrderDto>> GetAll(int pageNumber, CancellationToken cancellationToken);
    Task<int> GetTotalConut(CancellationToken cancellationToken);
    Task<Result> ChangeStateToWaitingForExpertOffer(int id, CancellationToken cancellationToken);
    Task<Result> ChangeStateToWaitingForExpertSelection(int id, CancellationToken cancellationToken);
    Task<Result> ChangeStateToWorkCompletedAndPaid(int id, CancellationToken cancellationToken);
 
    Task<Result> ChangeStateToCompleted(int id, CancellationToken cancellationToken);
    Task<List<GetOrderDto>> Search(string text, CancellationToken cancellationToken);
    //
    Task<List<GettOrderOverViewDto>> GetLatestOrders(CancellationToken cancellationToken);
     Task<Result> Delete(int id, CancellationToken cancellationToken);
    Task<GetOrderDto?> GetById(int id, CancellationToken cancellationToken);
    Task<int> GetActiveOrdersCountByExpert(int expertId, CancellationToken cancellationToken);
    Task<int> GetActiveOrdersCountByCustomer(int customerId, CancellationToken cancellationToken);
    Task<List<GettOrderOverViewDto>> GetCustomerOrders(int CustomerId, CancellationToken cancellationToken);
    Task<Result> Create(CreateOrderDto order, CancellationToken cancellationToken);
   
    Task<Result> Update(UpdateOrderDto model, CancellationToken cancellationToken);
    Task<GetFinalOrderDto?> GetFinalInfoById(int id, CancellationToken cancellationToken);
    Task<OrderStatusEnum> GetLastStatusOfOrder(int id, CancellationToken cancellationToken);
    Task<List<GetOrderDto>> GetAvailableOrdersForExpert(int expertId, int cityId, List<int> subserviceIds, CancellationToken cancellationToken);

}
