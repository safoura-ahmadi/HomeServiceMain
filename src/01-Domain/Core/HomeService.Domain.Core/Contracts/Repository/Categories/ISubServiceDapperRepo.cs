using HomeService.Domain.Core.Dtos.Categories;

namespace HomeService.Domain.Core.Contracts.Repository.Categories;

public interface ISubServiceDapperRepo
{
    Task<List<GetSubServiceDto>> GetAll(CancellationToken cancellationToken);
}
