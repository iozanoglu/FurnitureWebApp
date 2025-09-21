using System.ComponentModel.DataAnnotations;
using Voba.Core.Entities;
using Voba.Core.Enums;
using Voba.WebUI.Models.OrderProduct;

namespace Voba.WebUI.Models.Order
{
    public class OrderRequestModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }  // Siparişi veren kullanıcı
        public required string ShippingAddress { get; set; }  // Kullanıcının gönderim adresi
        public string? CompanyName { get; set; } // Siparişi veren şirketin ismi
        public int OrderTypeId { get; set; }
        public double? TotalPrice { get; set; } // Siparişin toplam fiyatı
        public double TotalSquareMeter { get; set; } // Siparişin toplam metrekare bilgisi
        public OrderStatus? Status { get; set; }
        public DateTime? OrderDate { get; set; } // Siparişin verildiği tarih
        public DateTime? ShipmentDate { get; set; } // Siparişin gönderildiği tarih

        public List<OrderProductRequestModel> OrderProducts { get; set; }
    }
}
