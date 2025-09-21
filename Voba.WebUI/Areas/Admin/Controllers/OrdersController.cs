using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Voba.Core.Entities;
using Voba.Core.Enums;
using Voba.Core.Services;
using Voba.Data;
using Voba.WebUI.Models.Order;
using Voba.WebUI.Models.OrderProduct;

namespace Voba.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminPolicy")]
    public class OrdersController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IOrderService _orderService;

        public OrdersController(DatabaseContext context, IOrderService orderService)
        {
            _context = context;
            _orderService = orderService;
        }

        // GET: Admin/Orders
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.Orders.Include(o => o.OrderType).Include(o => o.User).ToList();
            return View(databaseContext);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderType)
                .Include(o => o.OrderProducts)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order); // Tek bir Order nesnesi gönder
        }


        // GET: Admin/Orders/Create
        public IActionResult Create()
        {
            ViewData["UserSelectList"] = new SelectList(_context.Users, "Id", "Name");
            ViewData["OrderTypeSelectList"] = new SelectList(_context.OrderTypes, "Id", "Name"); // Burayı ekledik
            return View();
        }


        // POST: Admin/Orders/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost([FromBody] OrderRequestModel order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string orderCode = await _orderService.GenerateOrderCodeAsync(order.UserId);
                    var newOrder = new Order
                    {
                        UserId = order.UserId,
                        //ProductId = order.ProductId,
                        OrderDate = DateTime.Now,
                        OrderCode = orderCode,
                        ShipmentDate = null,
                        ShippingAddress = order.ShippingAddress,
                        CompanyName = order.CompanyName,
                        OrderTypeId = order.OrderTypeId,
                        TotalAmount = order.TotalPrice,
                        TotalSquareMeter = order.TotalSquareMeter,
                    };
                    _context.Add(newOrder);
                    await _context.SaveChangesAsync();
                    foreach (var orderProduct in order.OrderProducts)
                    {

                        OrderProduct newOrderProduct = new OrderProduct
                        {
                            OrderId = newOrder.Id,
                            //ProductId = orderProduct.ProductId,
                            CreatedBy = order.UserId,
                            Quantity = orderProduct.Quantity,
                            Length = orderProduct.Length,
                            Width = orderProduct.Width,
                            Color = orderProduct.Color,
                            Price = orderProduct.Price,
                            ModelName = orderProduct.ModelName,
                            SquareMeter = orderProduct.SquareMeter,
                            SquareMeterPrice = orderProduct.SquareMeterPrice.Value,
                            Description = orderProduct.Description

                        };
                        _context.Add(newOrderProduct);
                        await _context.SaveChangesAsync();

                    }

                    return RedirectToAction(nameof(Index));
                }
                //ViewData["ProductId"] = new SelectList(_context.Products, "Id", "ProductCode", orders.ProductId);
                //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", orders.UserId);
                return Ok(order);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderType)
                .Include(o => o.OrderProducts) // OrderProducts'ları da dahil et
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return NotFound();

            // Kullanıcı listesi
            ViewData["UserList"] = new SelectList(_context.Users, "Id", "Name", order.UserId);

            // Enum Status
            ViewBag.StatusList = Enum.GetValues(typeof(OrderStatus))
                .Cast<OrderStatus>()
                .Select(s => new SelectListItem
                {
                    Value = ((int)s).ToString(),
                    Text = s.ToString()
                }).ToList();

            return View(order);
        }



        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromBody]OrderRequestModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var order = await _context.Orders.Include(o => o.OrderProducts).FirstOrDefaultAsync(o => o.Id == vm.Id);
            if (order == null) return NotFound();

            // Ana sipariş bilgileri
            order.UserId = vm.UserId;
            order.OrderTypeId = vm.OrderTypeId;
            order.ShippingAddress = vm.ShippingAddress;
            order.CompanyName = vm.CompanyName;
            order.Status = vm.Status.Value;
            order.OrderDate = vm.OrderDate.Value;
            order.ShipmentDate = vm.ShipmentDate;
            order.TotalAmount = vm.TotalPrice;
            order.TotalSquareMeter = vm.TotalSquareMeter;

            var incoming = vm.OrderProducts.ToList() ?? new List<OrderProductRequestModel>();

            foreach (var d in incoming)
            {
                d.SquareMeter = Math.Round((d.Length * d.Width * d.Quantity) / 10000m, 4);
                d.Price = Math.Round(d.SquareMeter * d.SquareMeterPrice.Value, 2);
                
                if (!d.Id.HasValue)
                {
                    OrderProduct orderProduct = new OrderProduct
                    {
                        CreatedBy = vm.UserId,
                        //ProductId = d.ProductId,
                        Quantity = d.Quantity,
                        Length = d.Length,
                        Width = d.Width,
                        Color = d.Color,
                        Price = d.Price,
                        ModelName = d.ModelName,
                        SquareMeter = d.SquareMeter,
                        SquareMeterPrice = d.SquareMeterPrice.Value,
                        Description = d.Description,
                        OrderId = order.Id
                    };
                    _context.OrderProducts.Add(orderProduct);
                }
                else
                {
                    OrderProduct current = _context.OrderProducts.FirstOrDefault(op => op.Id == d.Id);
                    current.SquareMeter = d.SquareMeter;
                    current.Price = d.Price;
                   
                    current.ModelName = d.ModelName;
                    current.Color = d.Color;
                    current.Length = d.Length;
                    current.Width = d.Width;
                    current.Quantity = d.Quantity;
                    current.SquareMeter = d.SquareMeter;
                    current.SquareMeterPrice = d.SquareMeterPrice.Value;
                    current.Price = d.Price;
                    current.Description = d.Description;
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




        // GET: Admin/Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                //.Include(o => o.Product)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
        // GET: Admin/Orders/LoadOrderTable
        public async Task<IActionResult> LoadOrderTable(int initialCount = 30)
        {
            return PartialView("Partials/_OrderTablePartial", initialCount);
        }

    }
}