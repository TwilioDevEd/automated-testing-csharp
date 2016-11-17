namespace TwilioStore.Interfaces.Domain
{
    public interface IOrderDetail
    {
        string ItemId { get; set; }
        string Description { get; set; }
        int Quantity { get; set; }
    }
}