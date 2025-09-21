namespace Voba.WebUI.Models.User.OrderProduct
{
    public class UserOrderProductRequestModel
    {
        public int Quantity { get; set; }   // Sipariş edilen ürün adedi

        public decimal Length { get; set; }  // Ürünün uzunluğu

        public decimal Width { get; set; }   // Ürünün genişliği

        public string Color { get; set; }    // Ürünün rengi
        public string ModelName { get; set; } // Ürünün modeli
        public decimal SquareMeter { get; set; } // Ürünün modeli
        public string? Description { get; set; } // Ürünün modeli
        public int? Id { get; set; }
        public int? OrderId { get; set; }
    }
}
