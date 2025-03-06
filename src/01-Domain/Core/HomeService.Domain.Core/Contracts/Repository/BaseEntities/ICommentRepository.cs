using HomeService.Domain.Core.Dtos.BaseEntities;
using HomeService.Domain.Core.Dtos.Orders;
using HomeService.Domain.Core.Entities;

namespace HomeService.Domain.Core.Contracts.Repository.BaseEntities;

public interface ICommentRepository
{
    Task<Result> Create(CreateCommentDto item, CancellationToken cancellationToken);
    Task<List<GetCommentDto>> GetByExpertId(int expertId, CancellationToken cancellationToken);
    Task<float> GetExpertScore(int expertId, CancellationToken cancellationToken);
    Task<List<GetCommentDto>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<int> GetTotalCount(CancellationToken cancellationToken);
    Task<Result> ChangeStatusToRejected(int id, CancellationToken cancellationToken);
    Task<Result> ChangeStatusToAccepted(int id, CancellationToken cancellationToken);
    Task<List<GetCommentDto>> Search(string text,CancellationToken cancellationToken);
   

}
