using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Voba.Core.Entities;
using Voba.Data;
using Voba.WebUI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Voba.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly DatabaseContext _context;

        public AccountController(DatabaseContext context)
        {
            _context = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignInAsync(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var account = await _context.Users.FirstOrDefaultAsync(x => x.Email == loginViewModel.Email & x.Password == loginViewModel.Password & x.IsActive);
                    if(account == null) 
                    {
                        ModelState.AddModelError("", "Giriş Başarısız");
                    }
                    else
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, account.Name),
                            new Claim(ClaimTypes.Role, account.IsAdmin ? "Admin" : "User"),
                            new Claim("Id", account.Id.ToString())
                        };
                        var userIdentity = new ClaimsIdentity(claims, "Login");
                        ClaimsPrincipal userPrincipal = new ClaimsPrincipal(userIdentity);
                        await HttpContext.SignInAsync(userPrincipal);
                        return Redirect(string.IsNullOrEmpty(loginViewModel.ReturnUrl) ? "/" : loginViewModel.ReturnUrl); // login sonrası ana sayfaya yönlendir
                    }
                }
                catch (Exception hata)
                {
                    ModelState.AddModelError("","Hata Oluştu");
                }
            }   
            return View();
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUpAsync(User user)
        {
            user.IsAdmin = false;
            user.IsActive = true;
            if (ModelState.IsValid)
            {
                await _context.AddAsync(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public async Task<IActionResult> SignOutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("SignIn");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
