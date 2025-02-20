using HomeService.Domain.Core.Contracts.AppService.EndPoint;
using HomeService.Domain.Core.Dtos.Categories;
using HomeService.Domain.Core.Entities.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeService.Endpoint.Razor.Areas.Admin.Pages.SubCategories
{
    [Authorize(Roles = "Admin")]
    public class UpdateModel(IAdminSubCategoryManagement appService) : PageModel
    {
        private readonly IAdminSubCategoryManagement _appService = appService;
        [BindProperty]
        public UpdateSubCategoryDto? SubCategory { get; set; }
        [BindProperty]
        public List<GetCategoryForAdminPageDto> Categories { get; set; } = [];
        public async Task OnGet(int id, CancellationToken cancellationToken)
        {
            SubCategory = await _appService.GetById(id, cancellationToken);
            Categories = await _appService.GetAllCategories(cancellationToken);

        }
        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                if (SubCategory is not null)
                {
                    var result = await _appService.Update(SubCategory, cancellationToken);
                    TempData[result.Success ? "SuccessMessage" : "ErrorMessage"] = result.Message;
                }
                return RedirectToPage("Index");
            }

            Categories = await _appService.GetAllCategories(cancellationToken);
            return Page();
        }
    }
}
