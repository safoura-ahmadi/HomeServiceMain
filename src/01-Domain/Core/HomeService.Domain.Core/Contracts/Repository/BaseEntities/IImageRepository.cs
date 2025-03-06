namespace HomeService.Domain.Core.Contracts.Repository.BaseEntities;

public interface IImageRepository
{
   Task<bool> Create(List<string> imgAddress, int orderId, CancellationToken cancellationToken);
}
