using HomeService.Domain.Core.Dtos.Orders;
using HomeService.Domain.Core.Entities;


namespace HomeService.Domain.Core.Contracts.AppService.Orders;
public interface IOrderAppService
{
    Task<List<GetAllOrderDto>> GetAll(int pageNumber, CancellationToken cancellationToken);
    Task<int> GetTotalConut(CancellationToken cancellationToken);
    Task<Result> ChangeStateToWaitingForExpertOffer(int id, CancellationToken cancellationToken);
    Task<Result> ChangeStateToWaitingForExpertSelection(int id, CancellationToken cancellationToken);
    Task<Result> ChangeStateToWorkCompletedAndPaid(int id, CancellationToken cancellationToken);
    Task<Result> ChangeStateToExpertArrivedAtLocation(int id, CancellationToken cancellationToken);
    Task<List<GetOrderDto>> Search(string text, CancellationToken cancellationToken);
    //
    Task<List<GetLastOrderDto>> GetLatestOrders(CancellationToken cancellationToken);
     Task<Result> Delete(int id, CancellationToken cancellationToken);
}
