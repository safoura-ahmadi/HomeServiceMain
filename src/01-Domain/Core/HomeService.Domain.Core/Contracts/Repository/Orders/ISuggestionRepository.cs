using HomeService.Domain.Core.Dtos.Orders;
using HomeService.Domain.Core.Entities;

namespace HomeService.Domain.Core.Contracts.Repository.Orders;

public interface ISuggestionRepository
{
    Task<Result> Create(SuggestionDto suggestion, CancellationToken cancellationToken);
    Task<List<SuggestionDto>> GetByOrderId(int orderId, CancellationToken cancellationToken);
    Task<Result> ChangeStatetoAccepted(int id, CancellationToken cancellationToken);
    Task<Result> IsOrderHaveActiveSuggestion(int orderId, CancellationToken cancellationToken);
    Task<Result> IsOrderHaveAcceptedSugestion(int orderId, CancellationToken cancellationToken);
    Task<List<SuggestionDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<int> GetTotalCount(CancellationToken cancellationToken);
    Task<Result> Delete(int id, CancellationToken cancellationToken);



}
