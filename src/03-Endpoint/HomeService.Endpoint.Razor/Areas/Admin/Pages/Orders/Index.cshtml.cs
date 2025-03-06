using HomeService.Domain.Core.Contracts.AppService.Orders;
using HomeService.Domain.Core.Dtos.Orders;
using HomeService.Domain.Core.Entities;
using HomeService.Domain.Core.Enums.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeService.Endpoint.Razor.Areas.Admin.Pages.Orders
{
    [Authorize(Roles = "Admin")]
    public class IndexModel(IOrderAppService appService) : PageModel
    {
        private readonly IOrderAppService _appService = appService;
        [BindProperty]
        public List<GetAllOrderDto> Orders { get; set; } = [];
        [BindProperty]
        public int OrderCount { get; set; }
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
            Orders = await _appService.GetAll(pageNumber, cancellationToken);
            OrderCount = await _appService.GetTotalConut(cancellationToken);
        }
        public IActionResult OnGetNextPage()
        {
            return RedirectToAction("Index", new { pageNumber = CurrentPage + 1 });
        }
        public IActionResult OnGetPreviousPage()
        {
            return RedirectToAction("Index", new { pageNumber = CurrentPage - 1 });
        }
        public async Task<IActionResult> OnPostChangeOrderStatus(int orderId, OrderStatusEnum status, CancellationToken cancellationToken)
        {
            var result = new Result(true);
            switch (status)
            {
               
                case OrderStatusEnum.WaitingForExpertOffer:
                result = await _appService.ChangeStateToWaitingForExpertOffer(orderId, cancellationToken);
                    break;
                case OrderStatusEnum.WaitingForExpertSelection:
                    result = await _appService.ChangeStateToWaitingForExpertSelection(orderId, cancellationToken);
                    break;
  
                case OrderStatusEnum.WorkCompletedAndPaid:
                    result = await _appService.ChangeStateToWorkCompletedAndPaid(orderId, cancellationToken);
                    break;
                default:
                    result = Result.Fail("وضعیت انتخاب شده برای سفارش نامعتبر است");
                    break;
            }
            TempData[result.Success ? "SuccessMessage" : "ErrorMessage"] = result.Message;

            return RedirectToPage();
        }
    }
}
