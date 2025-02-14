using HomeService.Domain.Core.Dtos.Users;

namespace HomeService.Domain.Core.Contracts.Repository.Users;

public interface IExpertRepository
{
    Task<bool> IsConfirmedByAdmin(int userId, CancellationToken cancellationToken);
    Task<GetUsertDto?> GetByUserId(int userId, CancellationToken cancellationToken);
    Task<bool> Update(GetUsertDto model, CancellationToken cancellationToken);
    Task<bool> ConfirmById(int id, CancellationToken cancellationToken);
    Task<bool> UnConfirmById(int id, CancellationToken cancellationToken);
    Task<List<GetUsertDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<int> GetTotalCount(CancellationToken cancellationToken);
    Task<bool> ChargeBalance(int expertId, decimal money, CancellationToken cancellationToken);

}
