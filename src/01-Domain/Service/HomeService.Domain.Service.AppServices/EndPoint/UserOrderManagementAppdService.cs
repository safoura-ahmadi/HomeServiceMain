using HomeService.Domain.Core.Contracts.AppService.EndPoint;
using HomeService.Domain.Core.Contracts.AppService.Users;
using HomeService.Domain.Core.Contracts.Service.BaseEntities;
using HomeService.Domain.Core.Contracts.Service.Orders;
using HomeService.Domain.Core.Contracts.Service.Users;
using HomeService.Domain.Core.Dtos.BaseEntities;
using HomeService.Domain.Core.Dtos.EndPoint;
using HomeService.Domain.Core.Dtos.Orders;
using HomeService.Domain.Core.Entities;

namespace HomeService.Domain.Service.AppServices.EndPoint;

public class UserOrderManagementAppdService(IUserService userService, ICommentService commentService, IOrderService orderService, ISuggestionService suggestionService) : IUserOrderManagement
{
    private readonly IUserService _userService = userService;
    private readonly ICommentService _commentService = commentService;
    private readonly IOrderService _orderService = orderService;
    private readonly ISuggestionService _suggestionService = suggestionService;

    public Task<Result> CreateComment(CreateCommentDto item, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<GettOrderOverViewDto>> GetCustomerOrders(int CustomerId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<GetFinalOrderDto?> GetOrderFinalInfoById(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<SuggestionDetailsDto?> GetSuggestionDetailById(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result> PaymentProcedure(PaymentDto model, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
