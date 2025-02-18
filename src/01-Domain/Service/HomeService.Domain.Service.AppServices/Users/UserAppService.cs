using HomeService.Domain.Core.Contracts.AppService.Users;
using HomeService.Domain.Core.Contracts.Service.Users;
using HomeService.Domain.Core.Dtos.Users;
using HomeService.Domain.Core.Entities;

namespace HomeService.Domain.Service.AppServices.Users;

public class UserAppService(IUserService userService) : IUserAppService
{
    private readonly IUserService _userService = userService;

    public async Task<Result> ChargeBalance(int id, decimal money, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return Result.Fail("کاربری با این مشخصات وجود ندارد");
        if (money <= 0)
            return Result.Fail("مقدار مبلغ مورد نظر نامعتبر است");
        return await _userService.ChargeBalance(id, money, cancellationToken);
    }

    public async Task<Result> ConfirmById(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return Result.Fail("کابری با این مشخصات وجود ندارد");
        return await _userService.ConfirmById(id, cancellationToken);
    }

    public async Task<List<UsertDto>> GetAll(int pageNumber, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0)
            pageNumber = 1;
        return await _userService.GetAll(pageNumber, 10, cancellationToken);
    }

    public async Task<bool> IsConfirmedByAdmin(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return false;
        return await _userService.IsConfirmedByAdmin(id, cancellationToken);
    }

    public async Task<Result> UnConfirmById(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return Result.Fail("کاربری با این مشخصات یافت نشد");
        return await _userService.UnConfirmById(id, cancellationToken);
    }

    public async Task<Result> Update(UsertDto model, CancellationToken cancellationToken)
    {
        if (model.Id <= 0)
            return Result.Fail("کاربری با این مشخصات یافت نشد");
        return await _userService.Update(model, cancellationToken);
    }

    public async Task<Result> WithdrawBalance(int id, decimal money, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return Result.Fail("کاربری با این شخصات یافت نشد");
        if (money <= 0)
            return Result.Fail("مقدار مبلغ مورد نظر نامعتبر است");
        return await _userService.WithdrawBalance(id, money, cancellationToken);
    }
}
