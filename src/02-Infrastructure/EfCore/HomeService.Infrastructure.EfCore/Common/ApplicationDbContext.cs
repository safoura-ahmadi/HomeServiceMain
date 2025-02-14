using HomeService.Domain.Core.Entities.BaseEntities;
using HomeService.Domain.Core.Entities.Categories;
using HomeService.Domain.Core.Entities.Orders;
using HomeService.Domain.Core.Entities.Users;
using HomeService.Infrastructure.EfCore.Configuration.BaseEntites;
using HomeService.Infrastructure.EfCore.Configuration.Categories;
using HomeService.Infrastructure.EfCore.Configuration.Orders;
using HomeService.Infrastructure.EfCore.Configuration.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Emit;


namespace HomeService.Infrastructure.EfCore.Common;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.ApplyConfiguration(new ExpertSubServiceConfiguration());
        builder.ApplyConfiguration(new CityConfiguration());
        builder.ApplyConfiguration(new CommentConfiguration());
        builder.ApplyConfiguration(new CategoryConfiguration());
        builder.ApplyConfiguration(new SubCategoryConfiguration());
        builder.ApplyConfiguration(new SubServiceConfiguration());
        builder.ApplyConfiguration(new OrderConfiguration());
        builder.ApplyConfiguration(new SuggestionConfiguration());
        builder.ApplyConfiguration(new CustomerConfiguration());
        builder.ApplyConfiguration(new ExpertConfiguration());
        builder.ApplyConfiguration(new AdminConfiguration());
        UserConfiguration.SeedUsers(builder);
        base.OnModelCreating(builder);

    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<Suggestion> Suggestions { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
    public DbSet<SubService> SubServices { get; set; }
    public DbSet<City> Cities { get; set; }
      public DbSet<Comment> Comments { get; set; }
    public DbSet<ExpertSubService> ExpertSubServices { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Expert> Experts { get; set; }
    public DbSet<Admin> Admins { get; set; }
   
}
