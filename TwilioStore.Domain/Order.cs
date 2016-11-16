using System.Collections.Generic;
using TwilioStore.Interfaces.Domain;

namespace TwilioStore.Domain
{
    public class Order : IOrder
    {
        public Order()
        {
            Items = new List<IOrderDetail>();
        }

        public string Id { get; set; }
        public ICustomer Customer { get; set; }
        public IList<IOrderDetail> Items { get; }
        public string TrackingNum { get; set; }
    }
}