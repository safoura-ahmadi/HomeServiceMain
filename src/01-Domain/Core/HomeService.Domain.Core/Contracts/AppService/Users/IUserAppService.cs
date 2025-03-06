using HomeService.Domain.Core.Dtos.Users;
using HomeService.Domain.Core.Entities;
using HomeService.Domain.Core.Entities.Users;
using HomeService.Domain.Core.Enums.Users;
using Microsoft.AspNetCore.Identity;

namespace HomeService.Domain.Core.Contracts.AppService.Users;

public interface IUserAppService
{
    Task<List<GetAllUserDto>> GetAll(int pageNumber, CancellationToken cancellationToken);
    Task<UserStatusEnum> IsConfirmedByAdmin(int id, CancellationToken cancellationToken);
    Task<Result> WithdrawBalance(int id, decimal money, CancellationToken cancellationToken);
    Task<Result> ChargeBalance(int id, decimal money, CancellationToken cancellationToken);
    Task<Result> ConfirmById(int id, CancellationToken cancellationToken);
    Task<Result> UnConfirmById(int id, CancellationToken cancellationToken);
    Task<IdentityResult> Register(CreateUserDto model, CancellationToken cancellationToken);
    Task<IdentityResult> Update(UpdateUsertDto model, CancellationToken cancellationToken);
    Task<IdentityResult> Login(string username, string password, CancellationToken cancellationToken);
    Task<UpdateUsertDto?> GetById(int id, CancellationToken cancellationToken);
    Task<GetUserStaticDataDto?> GetUserStaticDate(int id, CancellationToken cancellationToken);
    Task Commit(CancellationToken cancellationToken);
}
