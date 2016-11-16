using System.Collections.Generic;

namespace TwilioStore.Interfaces.Domain
{
    public interface IOrder
    {
        string Id { get; set; }
        ICustomer Customer { get; set; }
        IList<IOrderDetail> Items { get; }
        string TrackingNum { get; set; }
    }
}