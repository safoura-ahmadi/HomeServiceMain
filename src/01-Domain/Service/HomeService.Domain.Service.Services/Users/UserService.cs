using HomeService.Domain.Core.Contracts.Repository.Users;
using HomeService.Domain.Core.Contracts.Service.Users;
using HomeService.Domain.Core.Dtos.Users;
using HomeService.Domain.Core.Entities;
using HomeService.Domain.Core.Enums.Users;

namespace HomeService.Domain.Service.Services.Users;

public class UserService(IUserRepository repository) : IUserService
{
    private readonly IUserRepository _repository = repository;

    public async Task<Result> ChargeBalance(int id, decimal money, CancellationToken cancellationToken)
    {
        return await _repository.ChargeBalance(id, money, cancellationToken);
    }

    public async Task<Result> ConfirmById(int id, CancellationToken cancellationToken)
    {
        return await _repository.ConfirmById(id, cancellationToken);
    }

    public async Task<List<GetAllUserDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(pageNumber, pageSize, cancellationToken);
    }

    public async Task<UserStatusEnum> IsConfirmedByAdmin(int id, CancellationToken cancellationToken)
    {
        return await _repository.IsConfirmedByAdmin(id, cancellationToken);
    }

    public async Task<Result> UnConfirmById(int id, CancellationToken cancellationToken)
    {
        return await _repository.UnConfirmById(id, cancellationToken);
    }

    public async Task<Result> Update(UpdateUsertDto model, CancellationToken cancellationToken)
    {
       return await _repository.Update(model, cancellationToken);
    }

    public async Task<Result> WithdrawBalance(int id, decimal money, CancellationToken cancellationToken)
    {
        return await _repository.WithdrawBalance(id, money, cancellationToken);
    }
}
