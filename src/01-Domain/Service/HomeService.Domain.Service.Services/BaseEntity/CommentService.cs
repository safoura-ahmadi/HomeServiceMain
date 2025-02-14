using HomeService.Domain.Core.Contracts.Repository.BaseEntities;
using HomeService.Domain.Core.Dtos.BaseEntities;

namespace HomeService.Domain.Service.Services.BaseEntity;

public class CommentService(ICommentRepository repository) : ICommentRepository
{
    private readonly ICommentRepository _repository = repository;

    public async Task<bool> Create(CreateCommentDto item, CancellationToken cancellationToken)
    {
        return await _repository.Create(item, cancellationToken);
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        return await _repository.Delete(id, cancellationToken);
    }

    public async Task<List<GetCommentDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(pageNumber, pageSize, cancellationToken);
    }

    public async Task<List<GetCommentDto>> GetByExpertId(int expertId, CancellationToken cancellationToken)
    {
       return await _repository.GetByExpertId(expertId,cancellationToken);
    }

    public async Task<float> GetExpertScore(int expertId, CancellationToken cancellationToken)
    {
        return await _repository.GetExpertScore(expertId,cancellationToken);
    }

    public async Task<int> GetTotalCount(CancellationToken cancellationToken)
    {
        return await _repository.GetTotalCount(cancellationToken);
    }
}
