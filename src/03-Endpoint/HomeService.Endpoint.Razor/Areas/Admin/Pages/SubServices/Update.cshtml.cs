using HomeService.Domain.Core.Contracts.AppService.Categories;
using HomeService.Domain.Core.Contracts.AppService.EndPoint;
using HomeService.Domain.Core.Dtos.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeService.Endpoint.Razor.Areas.Admin.Pages.SubServices
{
    [Authorize(Roles = "Admin")]
    public class UpdateModel(IAdminSubserviceManagement appService) : PageModel
    {
        private readonly IAdminSubserviceManagement _appService = appService;
        [BindProperty]
        public UpdateSubServiceDto? CreateModel { get; set; }
        [BindProperty]
        public List<GetSubCategoryDto> SubCategories { get; set; } = [];

        public async Task OnGet(int id, CancellationToken cancellationToken)
        {
            CreateModel = await _appService.GetById(id, cancellationToken);
            SubCategories = await _appService.GetAllSubCategories(cancellationToken);
        }
        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                if (CreateModel is not null)
                {
                    var result = await _appService.Update(CreateModel, cancellationToken);
                    TempData[result.Success ? "SuccessMessage" : "ErrorMessage"] = result.Message;
                }
                return RedirectToPage("Index");
            }
          
            SubCategories = await _appService.GetAllSubCategories(cancellationToken);
            return Page();
        }
    }
}
