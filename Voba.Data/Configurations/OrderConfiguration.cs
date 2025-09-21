using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voba.Core.Entities;
using Voba.Core.Enums;

namespace Voba.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Sipariş için gerekli alanların konfigürasyonu
            
            builder.Property(o => o.OrderDate).IsRequired();
            builder.Property(o => o.ShippingAddress).IsRequired().HasMaxLength(250);
            builder.Property(o => o.CompanyName).HasMaxLength(150); // Opsiyonel olan şirket adı


            // Varsayılan veriyi eklemek (opsiyonel)
            builder.HasData(
                new Order
                {
                    Id = 1,
                    UserId = 1,  // Örnek UserId
                    //ProductId = 1,  // Örnek ProductId
                   
                    //OrderDate = DateTime.UtcNow,
                    ShippingAddress = "123 Main Street",
                    Status = OrderStatus.Beklemede,
                });
        }
    }
}
