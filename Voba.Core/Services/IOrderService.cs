namespace Voba.Core.Services
{
    public interface IOrderService
    {
        Task<string> GenerateOrderCodeAsync(int userId);
    }
}
