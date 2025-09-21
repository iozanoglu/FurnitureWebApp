using System.ComponentModel.DataAnnotations;

namespace MyApp.Core.Entities
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string ModelName { get; set; }  // Model adı 
        public int Quantity { get; set; }   // Sipariş edilen ürün adedi
        public decimal Length { get; set; }  // Ürünün uzunluğu
        public decimal Width { get; set; }   // Ürünün genişliği
        public decimal SquareMeter { get; set; } // Ürünün metrekare cinsinden alanı
        public decimal SquareMeterPrice { get; set; } // Ürünün metrekare cinsinden alanı
        public decimal? Price { get; set; }  // Siparişin toplam tutarı
        public string Color { get; set; }    // Ürünün rengi
        public string? Description { get; set; }   // Açıklama
        public bool IsActive { get; set; } = true; // Siparişin aktif durumu
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public int CreatedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
