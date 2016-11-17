using Twilio;
using TwilioStore.Interfaces.Exceptions;
using TwilioStore.Interfaces.Services;

namespace TwilioStore.Services
{
    public class NotificationService : INotificationService
    {
        private readonly TwilioRestClient _client;
        private readonly INotificationConfiguration _config;

        public NotificationService(INotificationConfiguration config = null)
        {
            _config = config ?? new NotificationConfiguration();
            _client = new TwilioRestClient(_config.AccountSid, _config.AuthToken);
        }

        public void SendText(string to, string message)
        {
            var result = _client.SendMessage(_config.DefaultFromPhoneNumber, to, message);
            if (result.RestException != null)
            {
                throw new NotificationException(result.RestException.Message);
            }
        }

        public void MakePhoneCall(string to, string voiceUrl)
        {
            var result = _client.InitiateOutboundCall(_config.DefaultFromPhoneNumber, to, voiceUrl);
            if (result.RestException != null)
            {
                throw new NotificationException(result.RestException.Message);
            }
        }

        public void BuyPhoneNumber(string number)
        {
            var result = _client.AddIncomingPhoneNumber(new PhoneNumberOptions
            {
                PhoneNumber = number
            });
            if (result.RestException != null)
            {
                throw new NotificationException(result.RestException.Message);
            }
        }
    }
}