using HomeService.Domain.Core.Contracts.Repository.Orders;
using HomeService.Domain.Core.Contracts.Service.Orders;
using HomeService.Domain.Core.Dtos.Orders;
using HomeService.Domain.Core.Entities;

namespace HomeService.Domain.Service.Services.Orders;

public class SuggestionService(ISuggestionRepository repository) : ISuggestionService
{
    private readonly ISuggestionRepository _repository = repository;

    public async Task<Result> ChangeStatetoAccepted(int id, CancellationToken cancellationToken)
    {
        return await _repository.ChangeStatetoAccepted(id, cancellationToken);
    }


    public async Task<Result> Create(SuggestionDto suggestion, CancellationToken cancellationToken)
    {
        return await _repository.Create(suggestion, cancellationToken);
    }

    public async Task<Result> Delete(int id, CancellationToken cancellationToken)
    {
        return await _repository.Delete(id, cancellationToken);
    }

    public async Task<List<SuggestionDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(pageNumber, pageSize, cancellationToken);
    }
    
 
    public async Task<List<GetSuggestionForOrderDto>> GetByOrderId(int orderId, CancellationToken cancellationToken)
    {
        return await _repository.GetByOrderId(orderId, cancellationToken);
    }

    public async Task<List<GetlastSuggestionDto>> GetLatestSuggestions(CancellationToken cancellationToken)
    {
        return await _repository.GetLatestSuggestions(cancellationToken);
    }

    public async Task<int> GetTotalCount(CancellationToken cancellationToken)
    {
        return await _repository.GetTotalCount(cancellationToken);
    }

    public async Task<Result> IsOrderHaveAcceptedSugestion(int orderId, CancellationToken cancellationToken)
    {
        return await _repository.IsOrderHaveAcceptedSugestion(orderId, cancellationToken);
    }

    public async Task<Result> IsOrderHaveActiveSuggestion(int orderId, CancellationToken cancellationToken)
    {
        return await _repository.IsOrderHaveActiveSuggestion(orderId, cancellationToken);
    }
}
