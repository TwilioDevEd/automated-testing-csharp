using System;
using TwilioStore.Interfaces.Domain;

namespace TwilioStore.Services.Exceptions
{
    public class InsufficientInventoryException : Exception
    {
        public IOrderDetail LineItem { get; }

        public InsufficientInventoryException(IOrderDetail lineItem)
        {
            LineItem = lineItem;
        }
    }
}