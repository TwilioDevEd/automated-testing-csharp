namespace TwilioStore.Interfaces.Services
{
    public interface INotificationConfiguration
    {
        string AccountSid { get; }
        string AuthToken { get; }
        string DefaultFromPhoneNumber { get; }
    }
}