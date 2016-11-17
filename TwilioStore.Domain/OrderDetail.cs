using TwilioStore.Interfaces.Domain;

namespace TwilioStore.Domain
{
    public class OrderDetail : IOrderDetail
    {
        public string ItemId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}