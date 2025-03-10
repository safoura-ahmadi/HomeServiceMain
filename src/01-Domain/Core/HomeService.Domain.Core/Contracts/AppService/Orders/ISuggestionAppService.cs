using HomeService.Domain.Core.Contracts.Service.BaseEntities;
using HomeService.Domain.Core.Contracts.Service.Orders;
using HomeService.Domain.Core.Dtos.Orders;
using HomeService.Domain.Core.Entities;

namespace HomeService.Domain.Core.Contracts.AppService.Orders;

public interface ISuggestionAppService
{
    Task<List<GetSuggestionForOrderDto>> GetByOrderId(int orderId, CancellationToken cancellationToken);
    //
    Task<List<GetlastSuggestionDto>> GetLatestSuggestions(CancellationToken cancellationToken);
    Task<SuggestionDetailsDto?> GetDetailById(int id, CancellationToken cancellationToken);
    Task<int> GetExpertActiveSuggestionsCount(int expertId, CancellationToken cancellationToken);
    Task<int> GetCustomerActiveSuggestionsCount(int customerId, CancellationToken cancellationToken);
    Task<SuggestionDetailsDto?> GetSuggestionDetailById(int id, CancellationToken cancellationToken);
    Task<SuggestionOverviewDto?> GetById(int id, CancellationToken cancellationToken);
    Task<Result> ChangeStatetoAccepted(int id, CancellationToken cancellationToken);
    Task<Result> Create(SuggestionDto suggestion, CancellationToken cancellationToken);
}
