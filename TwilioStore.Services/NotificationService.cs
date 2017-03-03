using System;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using TwilioStore.Interfaces.Services;
using TwilioStore.Services.Exceptions;

namespace TwilioStore.Services
{
    public class NotificationService : INotificationService
    {
        private readonly TwilioRestClient _client;
        private readonly INotificationConfiguration _config;

        public NotificationService() : this(new NotificationConfiguration())
        {
        }

        public NotificationService(INotificationConfiguration config)
        {
            _config = config;
            _client = new TwilioRestClient(_config.AccountSid, 
                _config.AuthToken);
        }

        public void SendText(string to, string message)
        {
            try
            {
                MessageResource.Create(
                    to: new PhoneNumber(to),
                    from: new PhoneNumber(_config.DefaultFromPhoneNumber),
                    body: message,
                    client: _client
                );
            }
            catch (Exception e)
            {
                throw new NotificationException(e.Message, e);
            }
        }

        public void MakePhoneCall(string to, string voiceUrl)
        {
            try
            {
                CallResource.Create(
                    to: new PhoneNumber(to),
                    from: new PhoneNumber(_config.DefaultFromPhoneNumber),
                    url: new Uri(voiceUrl),
                    client: _client
                );
            }
            catch (Exception e)
            {
                throw new NotificationException(e.Message, e);
            }
        }

        public void BuyPhoneNumber(string number)
        {
            try
            {
                IncomingPhoneNumberResource.Create(
                    phoneNumber: new PhoneNumber(number),
                    client: _client
                );
            }
            catch (Exception e)
            {
                throw new NotificationException(e.Message, e);
            }
        }
    }
}