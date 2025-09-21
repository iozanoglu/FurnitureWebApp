using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection;
using MyApp.Core.Entities;
using MyApp.Data.Configurations;

namespace MyApp.Data
{
    public class DatabaseContext : DbContext
    {
        // Constructor ekleniyor
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        // Parametresiz constructor (opsiyonel, lokal test için)
        public DatabaseContext()
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<OrderType> OrderTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Lokal veritabanı
                optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=VobaDb;Trusted_Connection=True;TrustServerCertificate=True;")
                    .ConfigureWarnings(warnings =>
                        warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(o => o.Status)
                .HasConversion<int>();

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>().Property(p => p.Boy).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<OrderDetail>().Property(p => p.En).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<OrderDetail>().Property(p => p.Metrekare).HasColumnType("decimal(18,4)");
            modelBuilder.Entity<OrderDetail>().Property(p => p.MetrekareFiyat).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<OrderDetail>().Property(p => p.ToplamFiyat).HasColumnType("decimal(18,2)");

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
