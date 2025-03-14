namespace HomeService.Domain.Core.Entities.Configs
{
    public class SiteSetting
    {
        public ConnectionString ConnectionString { get; set; } = null!;
        public int SiteFeePercent { get; set; }
        public string ApiKey { get; set; } = null!;
    }
}
