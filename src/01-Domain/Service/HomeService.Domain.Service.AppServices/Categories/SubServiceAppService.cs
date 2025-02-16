using HomeService.Domain.Core.Contracts.AppService.Categories;
using HomeService.Domain.Core.Contracts.Service.Categories;
using HomeService.Domain.Core.Dtos.Categories;
using HomeService.Domain.Core.Entities;

namespace HomeService.Domain.Service.AppServices.Categories;

public class SubServiceAppService(ISubServiceService subService) : ISubServiceAppService
{
    private readonly ISubServiceService _subService = subService;

    public  async Task<Result> Create(CreateSubServiceDto model, CancellationToken cancellationToken)
    {
        return await _subService.Create(model, cancellationToken);
    }

    public async Task<Result> Delete(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return Result.Fail("هوم سرویسی با این ایدی موجود نیست");
        return await _subService.Delete(id, cancellationToken);
    }

    public async Task<List<GetSubServiceDto>> GetAll(int pageNumber, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0)
            pageNumber = 1;
        return await _subService.GetAll(pageNumber, 10, cancellationToken);
    }

    public async Task<int> GetTotalConut(CancellationToken cancellationToken)
    {
        return await _subService.GetTotalCount(cancellationToken);
    }

    public async Task<Result> Update(CreateSubServiceDto model, CancellationToken cancellationToken)
    {
        if (model.Id <= 0)
            return Result.Fail("هوم سرویسی با این ایدی موجود نیست");
        return await _subService.Update(model, cancellationToken);
    }
}
