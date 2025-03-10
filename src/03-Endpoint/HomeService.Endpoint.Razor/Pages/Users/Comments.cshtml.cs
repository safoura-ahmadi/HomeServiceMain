using HomeService.Domain.Core.Contracts.AppService.BaseEntities;
using HomeService.Domain.Core.Dtos.BaseEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeService.Endpoint.Razor.Pages.Users
{

    public class CommentsModel(ICommentAppService appService) : PageModel
    {
        private readonly ICommentAppService _appService = appService;
        [BindProperty]
        public List<GetCommentDto> Comments { get; set; } = [];
        public async Task<IActionResult> OnGet(int id, CancellationToken cancellationToken)
        {
            if (Request.Cookies["isConfirmed"] != "True")
            {
                return Redirect("~/Account/AccessDenied");
            }
            Comments = await _appService.GetByExpertId(id, cancellationToken);
            return Page();
        }
    }
}
