using HomeService.Domain.Core.Contracts.AppService.EndPoint;
using HomeService.Domain.Core.Dtos.EndPoint;
using HomeService.Domain.Core.Dtos.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeService.Endpoint.Razor.Areas.Admin.Pages
{
    [Authorize(Roles = "Admin")]
    public class IndexModel(IAdminIndexPageAppService adminIndexPageAppService) : PageModel
    {
        private readonly IAdminIndexPageAppService _adminIndexPageAppService = adminIndexPageAppService;
        [BindProperty]
        public StatisticsDataDto StaticData { get; set; } = null!;
        [BindProperty]
        public List<GetlastSuggestionDto> LastSuggestions { get; set; } = [];

        [BindProperty]
        public List<GetLastOrderDto> LastOrders { get; set; } = [];
        public async Task OnGet(CancellationToken cancellationToken)
        {
            StaticData = await _adminIndexPageAppService.GetStatisticsData(cancellationToken);
            LastSuggestions = await _adminIndexPageAppService.GetLatestSuggestions(cancellationToken);
            LastOrders = await _adminIndexPageAppService.GetLatestOrders(cancellationToken);

        }
    }
}
