using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Voba.Data;
using Voba.Core.Entities;
using Voba.Core.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using Voba.WebUI.Models.Order;
using Voba.Core.Services;
using Voba.WebUI.Models.User.Orders; // OrderStatus enum'ı için

[Authorize]
public class UserOrdersController : Controller
{
    private readonly DatabaseContext _context;
    private readonly IOrderService _orderService;

    public UserOrdersController(DatabaseContext context, IOrderService orderService)
    {
        _context = context;
        _orderService = orderService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = int.Parse(User.FindFirst("Id").Value);

        var orders = await _context.Orders
            .Include(o => o.OrderType)
            .Include(o => o.User)
            .Where(o => o.UserId == userId)
            .OrderBy(o => o.Status == OrderStatus.Beklemede ? 0 :
                          o.Status == OrderStatus.İşletmede ? 1 :
                          o.Status == OrderStatus.SevkiyataHazır ? 2 :
                          o.Status == OrderStatus.TeslimEdildi ? 3 : 4)
            .ThenByDescending(o => o.OrderDate) // aynı durumda eski siparişler yukarı
            .ToListAsync();

        return View(orders);
    }

    public async Task<IActionResult> Details(int id)
    {
        var userId = int.Parse(User.FindFirst("Id").Value);
        var order = await _context.Orders
            .Include(op => op.OrderProducts)
            .FirstOrDefaultAsync(op => op.Id == id && op.UserId == userId);

        if (order==null)
            return NotFound();

        order.TotalAmount = (double?)order.OrderProducts.Sum(op => op.Price);

        return View(order);
    }

    // GET: Admin/Orders/Create
    public IActionResult Create()
    {
        ViewData["UserSelectList"] = new SelectList(_context.Users, "Id", "Name");
        ViewData["OrderTypeSelectList"] = new SelectList(_context.OrderTypes, "Id", "Name");
        return View();
    }

    // POST: Admin/Orders/Create
    [HttpPost]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> CreatePost([FromBody] UserOrderRequestModel order)
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
                        ModelName = orderProduct.ModelName,
                        SquareMeter = orderProduct.SquareMeter,
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

}
