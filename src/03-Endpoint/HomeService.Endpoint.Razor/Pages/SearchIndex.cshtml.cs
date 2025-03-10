using HomeService.Domain.Core.Contracts.AppService.Categories;
using HomeService.Domain.Core.Dtos.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeService.Endpoint.Razor.Pages
{
    public class SearchIndexModel(ISubServiceAppService subServiceAppService) : PageModel
    {
        private readonly ISubServiceAppService _subServiceAppService = subServiceAppService;
        [BindProperty]
        public List<GetSubServiceDto> Subservices { get; set; } = [];
        public async Task<IActionResult> OnPost(string text, CancellationToken cancellationToken)

        {
            if (Request.Cookies["isConfirmed"] != "True")
            {
                return RedirectToPage("AccessDenied", new { area = "Account" });
            }


            Subservices = await _subServiceAppService.Search(text, cancellationToken);
            return Page();


        }
    }
}
