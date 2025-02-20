using HomeService.Domain.Core.Contracts.AppService.Orders;
using HomeService.Domain.Core.Dtos.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeService.Endpoint.Razor.Areas.Admin.Pages.Suggestions
{
    [Authorize(Roles = "Admin")]
    public class IndexModel(ISuggestionAppService appService) : PageModel
    {
        private readonly ISuggestionAppService _appService = appService;
        [BindProperty]
        public List<GetSuggestionForOrderDto> Suggestions { get; set; } = [];
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public async Task OnGet(CancellationToken cancellationToken)
        {
            Suggestions = await _appService.GetByOrderId(Id, cancellationToken);
        }
    }
}
