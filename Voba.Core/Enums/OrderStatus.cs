using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Core.Enums
{
    public enum OrderStatus
    {
        Beklemede = 0,      // Bekliyor
        İşletmede = 1, // Sipariş Alındı
        SevkiyataHazır = 2, // Üretimde
        TeslimEdildi = 3,      // Kargoya verildi
    }
}
