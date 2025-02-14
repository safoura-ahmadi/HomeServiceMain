using HomeService.Domain.Core.Contracts.Repository.Users;
using HomeService.Domain.Core.Contracts.Service.Users;
using HomeService.Domain.Core.Dtos.Users;

namespace HomeService.Domain.Service.Services.Users;

public class ExpertService(IExpertRepository repository) : IExpertService
{
    private readonly IExpertRepository _repository = repository;

    public async Task<bool> ChargeBalance(int expertId, decimal money, CancellationToken cancellationToken)
    {
        return await _repository.ChargeBalance(expertId, money, cancellationToken);
    }

    public async Task<bool> ConfirmById(int id, CancellationToken cancellationToken)
    {
        return await _repository.ConfirmById(id, cancellationToken);
    }

    public async Task<List<GetUsertDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(pageNumber,pageSize,cancellationToken);
    }

    public async Task<GetUsertDto?> GetByUserId(int userId, CancellationToken cancellationToken)
    {
        return await _repository.GetByUserId(userId, cancellationToken);
    }

    public async Task<int> GetTotalCount(CancellationToken cancellationToken)
    {
        return await _repository.GetTotalCount(cancellationToken);
    }

    public async Task<bool> IsConfirmedByAdmin(int userId, CancellationToken cancellationToken)
    {
        return await _repository.IsConfirmedByAdmin(userId, cancellationToken);
    }

    public async Task<bool> UnConfirmById(int id, CancellationToken cancellationToken)
    {
        return await _repository.UnConfirmById(id, cancellationToken);
    }

    public async Task<bool> Update(GetUsertDto model, CancellationToken cancellationToken)
    {
        return await _repository.Update(model, cancellationToken);
    }
}
