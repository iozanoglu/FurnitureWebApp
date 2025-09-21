using Voba.Core.Services;
using Voba.Data;
using Voba.Service.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Voba.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Database baðlantýsýný ekledik
            builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                   .AddCookie(x =>
                   {
                       x.LoginPath = "/Account/SignIn";
                       x.AccessDeniedPath = "/Account/AccessDenied";
                       x.Cookie.Name = "Account";
                       x.Cookie.MaxAge = TimeSpan.FromDays(30);
                       x.Cookie.IsEssential = true;
                   });

            builder.Services.AddAuthorization(x =>
            {
                x.AddPolicy("AdminPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
                x.AddPolicy("UserPolicy", policy => policy.RequireRole(ClaimTypes.Role, "Admin", "User"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "admin",
                pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
            );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
