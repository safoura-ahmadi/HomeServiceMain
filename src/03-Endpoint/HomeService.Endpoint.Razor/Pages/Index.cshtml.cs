using HomeService.Domain.Core.Contracts.AppService.Categories;
using HomeService.Domain.Core.Dtos.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeService.Endpoint.Razor.Pages
{
    public class IndexModel(ICategoryAppService categoryAppService) : PageModel
    {
        private readonly ICategoryAppService _categoryAppService = categoryAppService;
        [BindProperty]
        public List<GetCategoryForMainPageDto> Categories { get; set; } = [];
        public async Task<IActionResult> OnGet(CancellationToken cancellationToken )
        {
            if (HttpContext.Session.GetString("isConfirmed") != "True")
            {
                return RedirectToPage("AccessDenied", new { area = "Account" });
            }
            Categories = await _categoryAppService.GetAllForMainPage(cancellationToken);
            return Page();
        }
    }
}
