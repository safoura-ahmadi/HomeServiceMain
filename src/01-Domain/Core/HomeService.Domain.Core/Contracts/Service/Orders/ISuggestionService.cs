using HomeService.Domain.Core.Dtos.Orders;

namespace HomeService.Domain.Core.Contracts.Service.Orders;

public interface ISuggestionService
{
    Task<bool> Create(SuggestionDto suggestion, CancellationToken cancellationToken);
    Task<List<SuggestionDto>> GetSuggestionByOrder(int orderId, CancellationToken cancellationToken);
    Task<bool> ChangeStatetoAccepted(int id, CancellationToken cancellationToken);
    Task<List<SuggestionDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<int> GetTotalCount(CancellationToken cancellationToken);
    Task<bool> Delete(int id, CancellationToken cancellationToken);


}
