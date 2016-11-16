namespace TwilioStore.Interfaces.Services
{
    public interface INotificationService
    {
        void SendText(string to, string message);
        void MakePhoneCall(string to, string voiceUrl);
        void BuyPhoneNumber(string number);
    }
}