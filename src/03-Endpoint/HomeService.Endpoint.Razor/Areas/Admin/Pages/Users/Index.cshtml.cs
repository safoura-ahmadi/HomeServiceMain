using Azure;
using HomeService.Domain.Core.Contracts.AppService.BaseEntities;
using HomeService.Domain.Core.Contracts.AppService.Users;
using HomeService.Domain.Core.Contracts.Service.BaseEntities;
using HomeService.Domain.Core.Dtos.Users;
using HomeService.Domain.Core.Entities.BaseEntities;
using HomeService.Domain.Service.AppServices.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeService.Endpoint.Razor.Areas.Admin.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class IndexModel(IUserAppService userAppService, ICityAppService cityAppService) : PageModel
    {
        private readonly IUserAppService _userAppService = userAppService;
        private readonly ICityAppService _cityAppService = cityAppService;

        [BindProperty]
        public CreateUserDto CreateModel { get; set; } = null!;
        [BindProperty]
        public List<GetAllUserDto> Users { get; set; } = [];

        [BindProperty]
        public static int CurrentPage { get; set; }
        [BindProperty]
        public int MyPage { get; set; }
        [BindProperty]
        public List<City> Cities { get; set; } = [];

        public async Task OnGet(CancellationToken cancellationToken, int pageNumber = 1)
        {
            if (pageNumber > 100 || pageNumber <= 0)
                pageNumber = 1;
            CurrentPage = pageNumber;
            MyPage = pageNumber;
            Users = await _userAppService.GetAll(pageNumber, cancellationToken);
            Cities = await _cityAppService.GetAll(cancellationToken);
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
                try
                {
                    var result = await _userAppService.Register(CreateModel, cancellationToken);

                    if (result.Succeeded)
                    {
                        TempData["SuccessMessage"] = "ثبت نام با موفقیت انجام شد";
                       
                    }
                    foreach (var error in result.Errors)
                    {
                        TempData["ErrorMessage"] = error.Description;
                        return RedirectToPage("Index");
                    }
                }
                catch
                {
                    TempData["ErrorMessage"] = "مشکلی در دیتا بیس وجود دارد";
                  
                }
                return RedirectToPage("Index");
            }
          
            return Page();
        }
    }
}
