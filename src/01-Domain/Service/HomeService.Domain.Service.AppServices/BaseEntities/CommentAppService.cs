using HomeService.Domain.Core.Contracts.AppService.BaseEntities;
using HomeService.Domain.Core.Contracts.Service.BaseEntities;
using HomeService.Domain.Core.Dtos.BaseEntities;
using HomeService.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeService.Domain.Service.AppServices.BaseEntities;

public class CommentAppService(ICommentService commentService) : ICommentAppService
{
    private readonly ICommentService _commentService = commentService;

    public async Task<List<GetCommentDto>> GetAll(int pageNumber, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0)
            pageNumber = 1;
        return await _commentService.GetAll(pageNumber, 10, cancellationToken);
    }
    public async Task<int> GetTotalCount(CancellationToken cancellationToken)
    {
        return await _commentService.GetTotalCount(cancellationToken);
    }
    public async Task<Result> SetInActive(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return Result.Fail("کامنتی با این مشخصات یافت نشد");
            return await _commentService.SetInActive(id, cancellationToken);

    }

    public async Task<List<GetCommentDto>> Search(string text, CancellationToken cancellationToken)
    {
        return await _commentService.Search(text, cancellationToken);
    }

    public async Task<float> GetExpertScore(int expertId, CancellationToken cancellationToken)
    {
        if (expertId <= 0)
            return 0;
        return await _commentService.GetExpertScore(expertId, cancellationToken);
    }
}
