
using HomeService.Domain.Core.Dtos.BaseEntities;
using HomeService.Domain.Core.Dtos.EndPoint;
using HomeService.Domain.Core.Dtos.Orders;
using HomeService.Domain.Core.Entities;

namespace HomeService.Domain.Core.Contracts.AppService.EndPoint;

public interface IUserOrderManagement
{
    Task<List<GettOrderOverViewDto>> GetCustomerOrders(int CustomerId, CancellationToken cancellationToken);
    Task<SuggestionDetailsDto?> GetSuggestionDetailById(int id, CancellationToken cancellationToken);
    Task<GetFinalOrderDto?> GetOrderFinalInfoById(int id, CancellationToken cancellationToken);
    Task<Result> PaymentProcedure(PaymentDto model,CancellationToken cancellationToken);
    Task<Result> CreateComment(CreateCommentDto item, CancellationToken cancellationToken);
}
