using HomeService.Domain.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeService.Infrastructure.EfCore.Configuration.Users
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            

            builder.HasOne(a => a.User)
                .WithOne(ap => ap.Admin);
            builder.HasData(
                new Admin()
                {
                    Id = 1,
                    UserId = 1,
                    Balance = 100000,
                    Fname = "safoura",
                    Lname = "ahmadi"

                }

                );
        }
    }
}
