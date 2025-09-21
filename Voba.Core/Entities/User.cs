using System.ComponentModel.DataAnnotations;

namespace MyApp.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Display(Name = "İsim")]
        public required string Name { get; set; }
        [Display(Name = "Kullanıcı Adı")]
        public required string Username { get; set; }
        [Display(Name = "Firma Adı")]
        public string? CompanyName { get; set; }
        [Display(Name = "Parola")]
        public required string Password { get; set; }
        [Display(Name = "E-Posta")]
        public string? Email { get; set; }
        [Display(Name = "Telefon")]
        public required string Phone { get; set; }
        [Display(Name = "Aktif ?")]
        public bool IsActive { get; set; } = true;
        [Display(Name = "Yönetici ?")]
        public bool IsAdmin { get; set; }
        [Display(Name = "Kayıt Tarihi"), ScaffoldColumn(false)]
        public DateTime CreationDate { get; set; }= DateTime.Now;  // Bu tarih kısmı daha sonra incelenmeli ??????
        [ScaffoldColumn(false)]
        public Guid? UserGuid { get; set; } = Guid.NewGuid();
    }
}
