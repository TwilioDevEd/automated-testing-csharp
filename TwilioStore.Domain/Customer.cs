using TwilioStore.Interfaces.Domain;

namespace TwilioStore.Domain
{
    public class Customer : ICustomer
    {
        public string Name { get; set; }
        public string MobileNumber { get; set; }
    }
}