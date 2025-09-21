using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Voba.Core.Entities;
using Voba.Core.Enums;
using Voba.Data; // DbContext'in namespace'i

namespace Voba.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminPolicy")]
    public class MainController : Controller
    {
        private readonly DatabaseContext _context;

        public MainController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Teslim edilenler hariç tüm siparişler
            var orders = await _context.Orders
                .Where(o => o.Status != OrderStatus.TeslimEdildi)
                .ToListAsync();

            // Durum bazında sayım
            var statusCounts = orders
                .GroupBy(o => o.Status)
                .Select(g => new StatusViewModel
                {
                    Status = g.Key,
                    Count = g.Count()
                })
                .ToList();

            return View(statusCounts);
        }
    }

    // ViewModel
    public class StatusViewModel
    {
        public OrderStatus Status { get; set; }
        public int Count { get; set; }
    }
}
