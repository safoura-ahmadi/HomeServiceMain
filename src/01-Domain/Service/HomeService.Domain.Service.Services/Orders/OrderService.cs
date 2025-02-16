using HomeService.Domain.Core.Contracts.Repository.Orders;
using HomeService.Domain.Core.Contracts.Service.Orders;
using HomeService.Domain.Core.Dtos.Orders;
using HomeService.Domain.Core.Entities;
using HomeService.Domain.Core.Enums.Orders;

namespace HomeService.Domain.Service.Services.Orders;

public class OrderService(IOrderRepository repository) : IOrderService
{
    private readonly IOrderRepository _repository = repository;

    public async Task<Result> ChangeStateToExpertArrivedAtLocation(int id, CancellationToken cancellationToken)
    {
        return await _repository.ChangeStateToExpertArrivedAtLocation(id,cancellationToken);
    }

    public async Task<Result> ChangeStateToWaitingForExpertOffer(int id, CancellationToken cancellationToken)
    {
        return await _repository.ChangeStateToWaitingForExpertOffer(id, cancellationToken);
    }

    public async Task<Result> ChangeStateToWaitingForExpertSelection(int id, CancellationToken cancellationToken)
    {
        return await _repository.ChangeStateToWaitingForExpertSelection(id, cancellationToken);
    }

    public async Task<Result> ChangeStateToWorkCompletedAndPaid(int id, CancellationToken cancellationToken)
    {
        return await _repository.ChangeStateToWorkCompletedAndPaid(id, cancellationToken);
    }

    public async Task<int> Create(CreateOrderDto order, CancellationToken cancellationToken)
    {
        return await _repository.Create(order, cancellationToken);
    }

    public async Task<Result> Delete(int id, CancellationToken cancellationToken)
    {
        return await _repository.Delete(id, cancellationToken);
    }

    public async Task<List<GetOrderDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(pageNumber, pageSize, cancellationToken);
    }

    public async Task<List<GetOrderDto>> GetAvailableOrdersForExpert(int cityId, int subserviceId, CancellationToken cancellationToken)
    {
        return await _repository.GetAvailableOrdersForExpert(cityId, subserviceId, cancellationToken);
    }

    public async Task<OrderStatusEnum> GetLastStatusOfOrder(int id, CancellationToken cancellationToken)
    {
        return await _repository.GetLastStatusOfOrder(id, cancellationToken);
    }

    public async Task<int> GetTotalConut(CancellationToken cancellationToken)
    {
        return await _repository.GetTotalConut(cancellationToken);
    }

    public async Task<List<GetOrderDto>> Search(string text, CancellationToken cancellationToken)
    {
        return await _repository.Search(text, cancellationToken);
    }

    public async Task<Result> SetFinalPrice(int id, int price, CancellationToken cancellationToken)
    {
        return await _repository.SetFinalPrice(id, price, cancellationToken);
    }

    public async Task<Result> SetFinalTimeToDone(int id, DateTime timeToDone, CancellationToken cancellationToken)
    {
       return await _repository.SetFinalTimeToDone(id, timeToDone, cancellationToken);
    }
}
