namespace HomeService.Domain.Core.Dtos.Users;

public class GetExpertBasicInfo
{
    public int CityId { get; set; }
    public List<int> Skills { get; set; } = [];
}
