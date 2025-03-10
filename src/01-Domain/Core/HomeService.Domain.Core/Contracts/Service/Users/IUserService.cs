using HomeService.Domain.Core.Dtos.Users;
using HomeService.Domain.Core.Entities;
using HomeService.Domain.Core.Entities.Users;
using HomeService.Domain.Core.Enums.Users;

namespace HomeService.Domain.Core.Contracts.Service.Users;

public interface IUserService
{
    Task<List<GetAllUserDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<UserStatusEnum> IsConfirmedByAdmin(int id, CancellationToken cancellationToken);
    Task<Result> WithdrawBalance(int id, decimal money, CancellationToken cancellationToken);
    Task<Result> ChargeBalance(int id, decimal money, CancellationToken cancellationToken);
    Task<Result> Update(UpdateUsertDto model, CancellationToken cancellationToken);
    Task<Result> ConfirmById(int id, CancellationToken cancellationToken);
    Task<Result> UnConfirmById(int id, CancellationToken cancellationToken);
    Task<UpdateUsertDto?> GetById(int id, CancellationToken cancellationToken);
    Task<GetUserStaticDataDto?> GetUserStaticDate(int id, CancellationToken cancellationToken);
    Task Commit(CancellationToken cancellationToken);
    Task<int> GetCityId(int id, CancellationToken cancellationToken);

}
