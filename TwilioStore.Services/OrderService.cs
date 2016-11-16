using System.Configuration;
using Twilio;
using TwilioStore.Interfaces.Domain;
using TwilioStore.Interfaces.Exceptions;
using TwilioStore.Interfaces.Services;

namespace TwilioStore.Services
{
    public class OrderService : IOrderService
    {
        private readonly INotificationService _notificationService;

        public OrderService(INotificationService notificationService = null)
        {
            _notificationService = notificationService ?? new NotificationService();
        }

        public void ProcessOrder(IOrder order)
        {
            TryReserveInventory(order);
            var trackingNum = ShipOrder(order);
            _notificationService.SendText(order.Customer.MobileNumber,
                $"Your order {order.Id} has shipped! Tracking: {trackingNum}");
        }

        private void TryReserveInventory(IOrder order)
        {
            try
            {
                ReserveInventory(order);
            }
            catch (InsufficientInventoryException e)
            {
                _notificationService.SendText(order.Customer.MobileNumber,
                    $"Sorry, we don't have enough {e.LineItem.Description} in stock. :(");
                throw;
            }
        }

        private void ReserveInventory(IOrder order)
        {
            foreach (var lineItem in order.Items)
            {
                ReserveInventoryForLineItem(lineItem);
            }
        }

        private void ReserveInventoryForLineItem(IOrderDetail lineItem)
        {
            // TODO: Reserve inventory in inventory service
        }
        private string ShipOrder(IOrder order)
        {
            // TODO: Call shipping service to ship the order
            return "FakeTrackingNum";
        }

    }
}