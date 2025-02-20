using HomeService.Domain.Core.Contracts.AppService.Categories;
using HomeService.Domain.Core.Dtos.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeService.Endpoint.Razor.Areas.Admin.Pages.Categories
{
    [Authorize(Roles = "Admin")]
    public class UpdateModel(ICategoryAppService appService) : PageModel
    {
        private readonly ICategoryAppService _appService = appService;
        [BindProperty]
        public UpdateCategoryDto? Category { get; set; }
        public async Task OnGet(int id, CancellationToken cancellationToken)
        {
            Category = await _appService.GetById(id, cancellationToken);


        }
        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                if (Category is not null)
                {
                    var result = await _appService.Update(Category, cancellationToken);
                    TempData[result.Success ? "SuccessMessage" : "ErrorMessage"] = result.Message;
                }
                return RedirectToPage("Index");
            }

    
            return Page();
        }
    }
}
