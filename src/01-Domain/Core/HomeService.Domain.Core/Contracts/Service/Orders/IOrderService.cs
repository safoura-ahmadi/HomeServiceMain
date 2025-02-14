using HomeService.Domain.Core.Dtos.Orders;
using System.Threading.Tasks;

namespace HomeService.Domain.Core.Contracts.Service.Orders;

public interface IOrderService
{
    Task<bool> Create(CreateOrderDto order, CancellationToken cancellationToken);
    Task<List<GetOrderDto>> GetForExpert(int cityId, int subserviceId, CancellationToken cancellationToken);
    Task<bool> ChangeStateToWaitingForExpertSelection(int id, CancellationToken cancellationToken);
    Task<bool> ChangeStateToWorkCompletedAndPaid(int id, CancellationToken cancellationToken);
    Task<bool> ChangeStateToExpertArrivedAtLocation(int id, DateTime FinaltimeToDone, int finalPrice, CancellationToken cancellationToken);
    Task<List<GetOrderDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<int> GetTotalConut(CancellationToken cancellationToken);
    Task<bool> Delete(int id, CancellationToken cancellationToken);
}
