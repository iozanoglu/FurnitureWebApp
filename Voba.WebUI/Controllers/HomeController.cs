using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Voba.Data;
using Voba.WebUI.Models;

namespace Voba.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _context;

        public HomeController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Sliders.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult ContactUs()
        {
            return View();
        }
        public IActionResult OurProducts()
        {
            return View();
        }
        public IActionResult Catalog()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
