using HomeService.Domain.Core.Contracts.AppService.BaseEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeService.Endpoint.Razor.Areas.Admin.Pages.Comments
{
    [Authorize(Roles = "Admin")]
    public class RejectModel(ICommentAppService appService) : PageModel
    {
        private readonly ICommentAppService _appService = appService;

        public async Task<IActionResult> OnGet(int id,CancellationToken cancellationToken)
        {
            var result = await _appService.ChangeStatusToRejected(id, cancellationToken);
            TempData[result.Success ? "SuccessMessage" : "ErrorMessage"] = result.Message;
            return RedirectToPage("Index");
        }
    }
}
