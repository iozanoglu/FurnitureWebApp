using Voba.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Voba.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired().HasColumnType("varchar(50)");
            builder.Property(x => x.Username).HasMaxLength(50).IsRequired().HasColumnType("varchar(50)");
            builder.Property(x => x.Password).HasMaxLength(50).IsRequired().HasColumnType("varchar(50)");
            builder.Property(x => x.Email).HasMaxLength(50).HasColumnType("varchar(50)");
            builder.Property(x => x.Phone).HasMaxLength(50).IsRequired().HasColumnType("varchar(50)");
            builder.HasData(
                new User
                {
                    Id = 1,
                    Name = "admin",
                    Username = "admin",
                    Password = "1234",
                    Email = "admin@gmail.com",
                    Phone= "1234567890",
                    UserGuid = Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3301"),
                });
        }
    }
}
