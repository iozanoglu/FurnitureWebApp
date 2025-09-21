using Microsoft.EntityFrameworkCore;
using Voba.Core.Services;
using Voba.Data;

namespace Voba.Service.Services
{
    public class OrderService : IOrderService
    {

        private readonly DatabaseContext _context;

        public OrderService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateOrderCodeAsync(int userId)
        {
            var lastOrder = await _context.Orders.Where(e=> e.UserId == userId)
            .OrderByDescending(o => o.OrderCode)
            .FirstOrDefaultAsync();

            int lastCode = 1000;

            if (lastOrder != null && int.TryParse(lastOrder.OrderCode, out int parsedCode))
            {
                lastCode = parsedCode;
            }

            return (lastCode + 1).ToString();
        
    }
    }
}
