using HomeService.Domain.Core.Contracts.Repository.Orders;
using HomeService.Domain.Core.Contracts.Service.Orders;
using HomeService.Domain.Core.Dtos.Orders;

namespace HomeService.Domain.Service.Services.Orders;

public class OrderService(IOrderRepository repository) : IOrderService
{
    private readonly IOrderRepository _repository = repository;

    public async Task<bool> ChangeStateToExpertArrivedAtLocation(int id, DateTime FinaltimeToDone, int finalPrice, CancellationToken cancellationToken)
    {
        return await _repository.ChangeStateToExpertArrivedAtLocation(id,FinaltimeToDone,finalPrice,cancellationToken);
    }

    public async Task<bool> ChangeStateToWaitingForExpertSelection(int id, CancellationToken cancellationToken)
    {
        return await _repository.ChangeStateToWaitingForExpertSelection(id, cancellationToken);
    }

    public async Task<bool> ChangeStateToWorkCompletedAndPaid(int id, CancellationToken cancellationToken)
    {
        return await _repository.ChangeStateToWorkCompletedAndPaid(id, cancellationToken);
    }

    public async Task<bool> Create(CreateOrderDto order, CancellationToken cancellationToken)
    {
        return await _repository.Create(order, cancellationToken);
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        return await _repository.Delete(id, cancellationToken);
    }

    public async Task<List<GetOrderDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(pageNumber, pageSize, cancellationToken);
    }

    public async Task<List<GetOrderDto>> GetForExpert(int cityId, int subserviceId, CancellationToken cancellationToken)
    {
        return await _repository.GetForExpert(cityId, subserviceId, cancellationToken);
    }

    public async Task<int> GetTotalConut(CancellationToken cancellationToken)
    {
        return await _repository.GetTotalConut(cancellationToken);
    }
}
