using HomeService.Domain.Core.Contracts.AppService.BaseEntities;
using HomeService.Domain.Core.Dtos.BaseEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeService.Endpoint.Razor.Areas.Admin.Pages.Comments
{
    [Authorize(Roles = "Admin")]
    public class GetByExpertModel(ICommentAppService appService) : PageModel
    {
        private readonly ICommentAppService _appService = appService;
        [BindProperty]
        public List<GetCommentDto> Comments { get; set; } = [];
        public async void OnGet(int id, CancellationToken cancellationToken)
        {
            Comments = await _appService.GetByExpertId(id, cancellationToken);
        }
    }
}
