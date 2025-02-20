using HomeService.Domain.Core.Contracts.AppService.Categories;
using HomeService.Domain.Core.Contracts.AppService.EndPoint;
using HomeService.Domain.Core.Dtos.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeService.Endpoint.Razor.Areas.Admin.Pages.SubServices
{
    [Authorize(Roles = "Admin")]
    public class IndexModel(IAdminSubserviceManagement appService) : PageModel
    {
        private readonly IAdminSubserviceManagement _appService = appService;
        [BindProperty]
        public List<GetSubServiceDto> SubServices { get; set; } = [];
        [BindProperty]
        public List<GetSubCategoryDto> SubCategories{ get; set; } = [];
        [BindProperty]
        public CreateSubServiceDto CreateModel { get; set; } = new();
        [BindProperty]
        public int SubServiceCount { get; set; }
        [BindProperty]
        public static int CurrentPage { get; set; }
        [BindProperty]
        public int MyPage { get; set; }

        public async Task OnGet(CancellationToken cancellationToken, int pageNumber = 1)
        {
            if (pageNumber > 100 || pageNumber <= 0)
                pageNumber = 1;
            CurrentPage = pageNumber;
            MyPage = pageNumber;
            SubServices = await _appService.GetAllSubservices(pageNumber, cancellationToken);
            SubServiceCount = await _appService.GetTotalConut(cancellationToken);
            SubCategories = await _appService.GetAllSubCategories(cancellationToken);
        }
        public IActionResult OnGetNextPage()
        {
            return RedirectToAction("Index", new { pageNumber = CurrentPage + 1 });
        }
        public IActionResult OnGetPreviousPage()
        {
            return RedirectToAction("Index", new { pageNumber = CurrentPage - 1 });
        }
        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var result = await _appService.Create(CreateModel, cancellationToken);
                TempData[result.Success ? "SuccessMessage" : "ErrorMessage"] = result.Message;
                return RedirectToPage("Index");
            }
            ViewData["ShowModal"] = true;
            return Page();
        }
    }
}
