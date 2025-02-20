using HomeService.Domain.Core.Contracts.AppService.EndPoint;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeService.Endpoint.Razor.Areas.Admin.Pages.SubCategories
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel(IAdminSubCategoryManagement appService) : PageModel
    {
        private readonly IAdminSubCategoryManagement _appService = appService;

        public async Task<IActionResult> OnGet(int id, CancellationToken cancellationToken)
        {
            var result = await _appService.Delete(id, cancellationToken);
            TempData[result.Success ? "SuccessMessage" : "ErrorMessage"] = result.Message;
            return RedirectToPage("Index");
        }
    }
}
