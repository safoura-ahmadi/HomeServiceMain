using HomeService.Domain.Core.Contracts.Repository.Orders;
using HomeService.Domain.Core.Contracts.Service.Orders;
using HomeService.Domain.Core.Dtos.Orders;

namespace HomeService.Domain.Service.Services.Orders;

public class SuggestionService(ISuggestionRepository repository) : ISuggestionService
{
    private readonly ISuggestionRepository _repository = repository;

    public async Task<bool> ChangeStatetoAccepted(int id, CancellationToken cancellationToken)
    {
        return await _repository.ChangeStatetoAccepted(id, cancellationToken);
    }


    public async Task<bool> Create(SuggestionDto suggestion, CancellationToken cancellationToken)
    {
        return await _repository.Create(suggestion, cancellationToken);
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        return await _repository.Delete(id, cancellationToken);
    }

    public async Task<List<SuggestionDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(pageNumber, pageSize, cancellationToken);
    }

    public async Task<List<SuggestionDto>> GetSuggestionByOrder(int orderId, CancellationToken cancellationToken)
    {
        return await _repository.GetSuggestionByOrder(orderId, cancellationToken);
    }

    public async Task<int> GetTotalCount(CancellationToken cancellationToken)
    {
        return await _repository.GetTotalCount(cancellationToken);
    }
}
