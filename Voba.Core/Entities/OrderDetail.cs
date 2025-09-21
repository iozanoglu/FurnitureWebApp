using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Core.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }

        // FK
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        // Alanlar (senin oluşturma sayfandaki isimlerle uyumlu)
        [MaxLength(200)]
        public string? Model { get; set; }

        [MaxLength(150)]
        public string? Renk { get; set; }

        // cm cinsinden — decimal tercih (hassasiyet için)
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Boy { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? En { get; set; }

        public int? Adet { get; set; }

        // Hesaplanan ama DB’de tutacağız (kolay rapor için)
        [Column(TypeName = "decimal(18,4)")]
        public decimal? Metrekare { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? MetrekareFiyat { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ToplamFiyat { get; set; }

        [MaxLength(500)]
        public string? Aciklama { get; set; }

        // Edit ekranında satır silme için soft-delete bayrağı (isteğe bağlı)
        [NotMapped]
        public bool IsDeleted { get; set; } = false;
    }
}
