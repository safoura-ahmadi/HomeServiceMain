using HomeService.Domain.Core.Contracts.AppService.Categories;
using HomeService.Domain.Core.Contracts.AppService.EndPoint;
using HomeService.Domain.Core.Dtos.Categories;
using HomeService.Domain.Service.AppServices.EndPoint;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeService.Endpoint.Razor.Areas.Admin.Pages.SubCategories
{
    [Authorize(Roles = "Admin")]
    public class IndexModel(IAdminSubCategoryManagement appService) : PageModel
    {
        private readonly IAdminSubCategoryManagement _appService = appService;
        [BindProperty]
        public List<GetSubCategoryDto> SubCategories { get; set; } = [];
        [BindProperty]
        public List<GetCategoryForAdminPageDto> Categories { get; set; } = [];
        [BindProperty]
        public UpdateSubCategoryDto CreateModel { get; set; } = new();

        public async Task OnGet(CancellationToken cancellationToken)
        {
            SubCategories = await _appService.GetAllSubCategories(cancellationToken);
            Categories = await _appService.GetAllCategories(cancellationToken);
        }
        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var result = await _appService.Create(CreateModel.Title, CreateModel.CategoryId, cancellationToken);
                TempData[result.Success ? "SuccessMessage" : "ErrorMessage"] = result.Message;
                return RedirectToPage("Index");
            }
            ViewData["ShowModal"] = true;
            return Page();
        }
    }
}
