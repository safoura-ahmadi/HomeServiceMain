using HomeService.Domain.Core.Contracts.AppService.BaseEntities;
using HomeService.Domain.Core.Dtos.BaseEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeService.Endpoint.Razor.Areas.Admin.Pages.Comments
{
    [Authorize(Roles = "Admin")]
    public class IndexModel(ICommentAppService appService) : PageModel
    {
        private readonly ICommentAppService _appService = appService;
        [BindProperty]
        public List<GetCommentDto> Comments { get; set; } = [];
        [BindProperty]
        public int CommentCount { get; set; }
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
            Comments = await _appService.GetAll(pageNumber, cancellationToken);
            CommentCount = await _appService.GetTotalCount(cancellationToken);
        }
        public IActionResult OnGetNextPage()
        {
            return RedirectToAction("Index", new { pageNumber = CurrentPage + 1 });
        }
        public IActionResult OnGetPreviousPage()
        {
            return RedirectToAction("Index", new { pageNumber = CurrentPage - 1 });
        }
    }
}
