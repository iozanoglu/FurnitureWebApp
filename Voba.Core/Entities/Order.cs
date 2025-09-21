using System.ComponentModel.DataAnnotations;
using MyApp.Core.Enums;

namespace MyApp.Core.Entities
{
    public class Order
    {
        public int Id { get; set; }
        [Display(Name = "Kulanıcı ID")]
        public int UserId { get; set; }  // Siparişi veren kullanıcı
        [Display(Name = "Kullanıcı")]
        public User? User { get; set; }    // Kullanıcıyla ilişki kuruyoruz
        public int OrderTypeId { get; set; }  // Siparişi veren kullanıcı
        [Display(Name = "Sipariş Türü")]
        public OrderType? OrderType { get; set; }    // Kullanıcıyla ilişki kuruyoruz
        public double? TotalAmount { get; set; } // Siparişin toplam tutarı
        public double TotalSquareMeter { get; set; } // Siparişin toplam metrekare bilgisi
        public string OrderCode { get; set; } = string.Empty; // Sipariş kodu
        [Display(Name = "Sipariş Tarihi")]
        public DateTime OrderDate { get; set; } = DateTime.Now;  // Sipariş tarihi
        [Display(Name = "Kargo Tarihi")]
        public DateTime? ShipmentDate { get; set; }  // Kargo tarihi, isteğe bağlı
        [Display(Name = "Kargo Adresi")]
        public required string ShippingAddress { get; set; }  // Kullanıcının gönderim adresi
        [Display(Name = "Şirket Adı")]
        public string? CompanyName { get; set; } // Siparişi veren şirketin ismi
        public bool IsActive { get; set; } = true;
        [Display(Name = "Durum")]
        public OrderStatus Status { get; set; } = OrderStatus.Beklemede; // Siparişin durumu
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    }

}
