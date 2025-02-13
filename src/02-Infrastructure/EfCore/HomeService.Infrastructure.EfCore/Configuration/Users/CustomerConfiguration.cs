
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HomeService.Domain.Core.Entities.Users;

namespace HomeService.Infrastructure.EfCore.Configuration.Users;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasOne(c => c.User)
            .WithOne(u => u.Customer);

        //builder.HasData(
        //    new Customer()
        //    {
        //        Id = 1,
        //        UserId = 3
        //    });
       

       
    }
}