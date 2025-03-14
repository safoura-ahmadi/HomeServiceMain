using Framework;
using HomeService.Domain.Core.Contracts.AppService.BaseEntities;
using HomeService.Domain.Core.Contracts.AppService.Categories;
using HomeService.Domain.Core.Contracts.AppService.EndPoint;
using HomeService.Domain.Core.Contracts.AppService.Orders;
using HomeService.Domain.Core.Contracts.AppService.Users;
using HomeService.Domain.Core.Contracts.Repository.BaseEntities;
using HomeService.Domain.Core.Contracts.Repository.Categories;
using HomeService.Domain.Core.Contracts.Repository.Orders;
using HomeService.Domain.Core.Contracts.Repository.Users;
using HomeService.Domain.Core.Contracts.Service.BaseEntities;
using HomeService.Domain.Core.Contracts.Service.Categories;
using HomeService.Domain.Core.Contracts.Service.Orders;
using HomeService.Domain.Core.Contracts.Service.Users;
using HomeService.Domain.Core.Entities.Configs;
using HomeService.Domain.Core.Entities.Users;
using HomeService.Domain.Service.AppServices.BaseEntities;
using HomeService.Domain.Service.AppServices.Categories;
using HomeService.Domain.Service.AppServices.EndPoint;
using HomeService.Domain.Service.AppServices.Orders;
using HomeService.Domain.Service.AppServices.Users;
using HomeService.Domain.Service.Services.BaseEntity;
using HomeService.Domain.Service.Services.Categories;
using HomeService.Domain.Service.Services.Orders;
using HomeService.Domain.Service.Services.Users;
using HomeService.Endpoint.Razor.Middleware;
using HomeService.Infrastructure.Dapper.Repositories.BaseEntites;
using HomeService.Infrastructure.Dapper.Repositories.Categories;
using HomeService.Infrastructure.EfCore.Common;
using HomeService.Infrastructure.EfCore.Repository.BaseEntities;
using HomeService.Infrastructure.EfCore.Repository.Categories;
using HomeService.Infrastructure.EfCore.Repository.Orders;
using HomeService.Infrastructure.EfCore.Repository.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureLogging(o => {
    o.ClearProviders();
    o.AddSerilog();
}).UseSerilog((context, config) =>
{
    config.WriteTo.Console();
    config.WriteTo.Seq("http://localhost:5341", apiKey: "xMRqcaEpiNfsu9qHIfOI");
});
try
{
    var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
    var siteSetting = configuration.GetSection("SiteSetting").Get<SiteSetting>();
    builder.Services.AddSingleton(siteSetting!);
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(siteSetting!.ConnectionString.SqlConnection));
    builder.Services.AddAuthentication("Authenticate")
    .AddCookie("Authenticate", options =>
    {
        options.AccessDeniedPath = "/Account/AccessDeniedModel";
    });
    // Add services to the container.
    builder.Services.AddMemoryCache();
    //builder.Services.AddDistributedMemoryCache();
    //builder.Services.AddStackExchangeRedisCache(options =>
    //{
    //    options.Configuration = builder.Configuration.GetConnectionString("172.0.0.1:9999");
    //    options.InstanceName = "SampleInstance";
    //});

    builder.Services.AddResponseCaching();
    builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
    builder.Services.AddScoped<IAdminIndexPageAppService, AdminIndexPageAppService>();
    builder.Services.AddScoped<IExpertAppService, ExpertAppService>();
    builder.Services.AddScoped<IExpertService, ExpertService>();
    builder.Services.AddScoped<IExpertRepository, ExpertEfRepository>();
    builder.Services.AddScoped<ICustomerAppService, CustomerAppService>();
    builder.Services.AddScoped<ICustomerService, CustomerService>();
    builder.Services.AddScoped<ICustomerRepository, CustomerEfRepository>();
    builder.Services.AddScoped<IOrderAppService, OrderAppService>();
    builder.Services.AddScoped<IOrderService, OrderService>();
    builder.Services.AddScoped<IOrderRepository, OrderEfRepository>();
    builder.Services.AddScoped<ISuggestionAppService, SuggestionAppService>();
    builder.Services.AddScoped<ISuggestionService, SuggestionService>();
    builder.Services.AddScoped<ISuggestionRepository, SuggestionEfRepository>();
    builder.Services.AddScoped<ICommentAppService, CommentAppService>();
    builder.Services.AddScoped<ICommentService, CommentService>();
    builder.Services.AddScoped<ICommentRepository, CommentEfRepository>();
    builder.Services.AddScoped<ISubServiceAppService, SubServiceAppService>();
    builder.Services.AddScoped<ISubServiceService, SubServiceService>();
    builder.Services.AddScoped<ISubServiceRepository, SubServiceEfRepository>();
    builder.Services.AddScoped<ISubServiceDapperRepo, SubServiceDapperRepository>();
    builder.Services.AddScoped<IImageReposiotry, ImageService>();
    builder.Services.AddScoped<IImageRepository, ImageEfRepository>();
    builder.Services.AddScoped<IAdminSubserviceManagement, AdminSubserviceManagement>();
    builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
    builder.Services.AddScoped<ISubCategoryRepository, SubCategoryEfRepository>();
    builder.Services.AddScoped<ISubCategoryDapperRepo, SubCategoryDapperRepository>();
    builder.Services.AddScoped<ICategoryAppService, CategoryAppService>();
    builder.Services.AddScoped<ICategoryService, CategoryService>();
    builder.Services.AddScoped<ICategoryRepository, CategoryEfRepository>();
    builder.Services.AddScoped<ICategoryDapperRepo, CategoryDapperRepository>();
    builder.Services.AddScoped<IAdminSubCategoryManagement, AdminSubCategoryManagement>();
    builder.Services.AddScoped<IUserAppService, UserAppService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IUserRepository, UserEfRepository>();
    builder.Services.AddScoped<ICityAppService, CityAppService>();
    builder.Services.AddScoped<ICityService, CityService>();
    builder.Services.AddScoped<ICityRepository, CityDapperRepository>();
    builder.Services.AddScoped<IExpertSubServiceAppservice, ExpertSubServiceAppService>();
    builder.Services.AddScoped<IExpertSubServiceService, ExpertSubServiceService>();
    builder.Services.AddScoped<IExpertSubServiceRepository, ExpertSubServiceEfRepository>();
    builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
    {
       
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    })
        .AddRoles<IdentityRole<int>>()
        .AddErrorDescriber<PersianIdentityErrorDescriber>()
        .AddEntityFrameworkStores<ApplicationDbContext>();
  

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseErrorLogging();
    app.UseHttpsRedirection();

    app.UseRouting();

    app.UseAuthorization();

    app.MapStaticAssets();
    app.MapRazorPages()
       .WithStaticAssets();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
