using Voba.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Voba.Data.Configurations
{
    internal class SliderConfiguration : IEntityTypeConfiguration<Slider>
    {
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            builder.Property(p => p.Title).HasMaxLength(100);
            builder.Property(p => p.Image).HasMaxLength(200);
        }
    }
}
