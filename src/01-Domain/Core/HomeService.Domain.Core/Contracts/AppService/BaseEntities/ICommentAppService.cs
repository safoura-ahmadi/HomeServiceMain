using HomeService.Domain.Core.Dtos.BaseEntities;
using HomeService.Domain.Core.Entities;

namespace HomeService.Domain.Core.Contracts.AppService.BaseEntities;

public interface ICommentAppService
{
    Task<float> GetExpertScore(int expertId, CancellationToken cancellationToken);
    Task<List<GetCommentDto>> GetAll(int pageNumber, CancellationToken cancellationToken);
    Task<int> GetTotalCount(CancellationToken cancellationToken);
    Task<Result> SetInActive(int id, CancellationToken cancellationToken);
    Task<List<GetCommentDto>> Search(string text, CancellationToken cancellationToken);
}
