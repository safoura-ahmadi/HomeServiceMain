using HomeService.Domain.Core.Dtos.BaseEntities;

namespace HomeService.Domain.Core.Contracts.Repository.BaseEntities;

public interface ICommentRepository
{
    Task<bool> Create(CreateCommentDto item, CancellationToken cancellationToken);
    Task<List<GetCommentDto>> GetByExpertId(int expertId, CancellationToken cancellationToken);
    Task<float> GetExpertScore(int expertId, CancellationToken cancellationToken);
    Task<List<GetCommentDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<int> GetTotalCount(CancellationToken cancellationToken);
    Task<bool> Delete(int id, CancellationToken cancellationToken);

}
