using HomeService.Domain.Core.Contracts.AppService.Users;
using HomeService.Domain.Core.Contracts.Service.BaseEntities;
using HomeService.Domain.Core.Contracts.Service.Users;
using HomeService.Domain.Core.Dtos.Users;
using HomeService.Domain.Core.Entities;
using HomeService.Domain.Core.Entities.Users;
using HomeService.Domain.Core.Enums.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace HomeService.Domain.Service.AppServices.Users;

public class UserAppService(IUserService userService, ICustomerAppService customerAppService, IExpertAppService expertAppService, UserManager<Core.Entities.Users.User> userManager, SignInManager<Core.Entities.Users.User> signInManager, IPasswordHasher<Core.Entities.Users.User> passwordHasher, ILogger<UserAppService> logger, IImageReposiotry imageService) : IUserAppService
{
    private readonly IUserService _userService = userService;
    private readonly ICustomerAppService _customerAppService = customerAppService;
    private readonly IExpertAppService _expertAppService = expertAppService;
    private readonly UserManager<Core.Entities.Users.User> _userManager = userManager;
    private readonly SignInManager<Core.Entities.Users.User> _signInManager = signInManager;
    private readonly IPasswordHasher<Core.Entities.Users.User> _passwordHasher = passwordHasher;
    private readonly ILogger<UserAppService> _logger = logger;
    private readonly IImageReposiotry _imageService = imageService;

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
            return Result.Fail("کاربری با این مشخصات وجود ندارد");
        return await _userService.ConfirmById(id, cancellationToken);
    }

    public async Task<List<GetAllUserDto>> GetAll(int pageNumber, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0)
            pageNumber = 1;
        return await _userService.GetAll(pageNumber, 10, cancellationToken);
    }

    public async Task<UserStatusEnum> IsConfirmedByAdmin(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return UserStatusEnum.Pending;
        _logger.LogInformation("have request to confirm user by {id}", id.ToString());
        return await _userService.IsConfirmedByAdmin(id, cancellationToken);
    }

    public async Task<Result> UnConfirmById(int id, CancellationToken cancellationToken)
    {
        _logger.Log(LogLevel.Warning, "Attempt to delete{item}", "User");
        if (id <= 0)
            return Result.Fail("کاربری با این مشخصات یافت نشد");
        _logger.LogInformation("have request to unconfirm user by {id}", id.ToString());
        return await _userService.UnConfirmById(id, cancellationToken);
    }



    public async Task<Result> WithdrawBalance(int id, decimal money, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return Result.Fail("کاربری با این شخصات یافت نشد");
        if (money <= 0)
            return Result.Fail("مقدار مبلغ مورد نظر نامعتبر است");
        return await _userService.WithdrawBalance(id, money, cancellationToken);
    }

    public async Task<IdentityResult> Register(CreateUserDto model, CancellationToken cancellationToken)
    {
        string role = string.Empty;


        var user = new Core.Entities.Users.User
        {
            UserName = model.Email,
            CityId = model.CityId,
            Status = UserStatusEnum.Pending,
            LockoutEnabled = false,
            Email = model.Email,
            Balance = 100000

        };

        if (model.Role == RoleEnum.Customer)
        {
            role = "Customer";
            user.Customer = new Customer()
            {
                CityId = model.CityId
            };

        }

        if (model.Role == RoleEnum.Expert)
        {
            role = "Expert";
            user.Expert = new Expert();
        }



        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {

            _logger.LogInformation("new user Register by {role}", role);
            await _userManager.AddToRoleAsync(user, role);
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, role));
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));
            await _userManager.AddClaimAsync(user, new Claim("Fname", user.Fname ?? "کاربر"));



            if (model.Role == RoleEnum.Customer)
            {
                await _userManager.AddClaimAsync(user, new Claim("CustomerId", user.Customer!.Id.ToString()));
            }

            if (model.Role == RoleEnum.Expert)
            {
                await _userManager.AddClaimAsync(user, new Claim("ExpertId", user.Expert!.Id.ToString()));
            }



        }

        return result;
    }
    public async Task<IdentityResult> Login(string username, string password, CancellationToken cancellationToken)
    {
        var result = await signInManager.PasswordSignInAsync(username, password, false, false);
        if (result.Succeeded)
            _logger.LogInformation("{username} have successful login", username.ToString());
        else
            _logger.LogWarning("{username} login proccess failed", username.ToString());
        return result.Succeeded ? IdentityResult.Success : IdentityResult.Failed();

    }

    public async Task<IdentityResult> Update(UpdateUsertDto model, CancellationToken cancellationToken)
    {

        if (model.Id <= 0 || await _userManager.FindByIdAsync(model.Id.ToString()) is not { } user)
            return IdentityResult.Failed(new IdentityError { Description = "کاربری با این مشخصات یافت نشد" });

        try
        {
            if (model.ImageFile is not null)
                model.ImagePath = await _imageService.UploadImage(model.ImageFile!, "icon", cancellationToken);


        }
        catch
        {
            return IdentityResult.Failed(new IdentityError { Description = "مشکلی در آپدیت عکس ایجاد شده" });

        }
        user.Fname = model.Fname ?? user.Fname;
        user.Lname = model.Lname ?? user.Lname;
        if (model.ImagePath is not null)
            user.ImagePath = model.ImagePath;
        user.Balance += model.Amount;
        user.Address = model.Address ?? user.Address;
        user.CityId = model.CityId;
        user.Mobile = model.Mobile ?? user.Mobile;



        return await _userManager.UpdateAsync(user);
    }

    public async Task<UpdateUsertDto?> GetById(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return null;
        return await _userService.GetById(id, cancellationToken);

    }



    public async Task<GetUserStaticDataDto?> GetUserStaticDate(int id, CancellationToken cancellationToken)
    {

        if (id <= 0)
            return null;
        return await _userService.GetUserStaticDate(id, cancellationToken);
    }

    public async Task Commit(CancellationToken cancellationToken)
    {
        await _userService.Commit(cancellationToken);
    }
}
