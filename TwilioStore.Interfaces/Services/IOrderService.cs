using TwilioStore.Interfaces.Domain;

namespace TwilioStore.Interfaces.Services
{
    public interface IOrderService
    {
        void ProcessOrder(IOrder order);
    }
}