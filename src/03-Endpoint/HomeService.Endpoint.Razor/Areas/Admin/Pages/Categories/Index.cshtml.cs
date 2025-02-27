using HomeService.Domain.Core.Contracts.AppService.Categories;
using HomeService.Domain.Core.Dtos.Categories;
using HomeService.Domain.Core.Dtos.Users;
using HomeService.Endpoint.Razor.Pages.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeService.Endpoint.Razor.Areas.Admin.Pages.Categories
{
    [Authorize(Roles = "Admin")]
    public class IndexModel(ICategoryAppService appService) : PageModel
    {
        private readonly ICategoryAppService _appService = appService;
      
        [BindProperty]
        public List<GetCategoryForAdminPageDto> Categories { get; set; } = [];
        [BindProperty]
        public string Title { get; set; } = null!;
        [BindProperty]
        public IFormFile ImageFile { get; set; } = null!;
        public async Task OnGet(CancellationToken cancellationToken)
        {
            if(User is null)
            {
                RedirectToPage("./AccessDenied");
            }
            Categories = await _appService.GetAll(cancellationToken);
        }
        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var result = await _appService.Create(Title, ImageFile, cancellationToken);
                TempData[result.Success ? "SuccessMessage" : "ErrorMessage"] = result.Message;
                return RedirectToPage("Index");
            }
            ViewData["ShowModal"] = true;
            return Page();
        }
    }
}
