using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TwilioStore.Interfaces.Services;
using TwilioStore.Services;
using TwilioStore.Services.Exceptions;

namespace TwilioStore.Tests.Services
{
    [TestClass]
    public class NotificationServiceTest
    {
        // TODO: Get your test credentials from 
        //   https://www.twilio.com/console/account/settings
        private static string _testAccountSid =
            "ACxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
        private static string _testAuthToken =
            "0123456789abcdef0123456789abcdef";

        // Magic numbers: https://www.twilio.com/docs/api/rest/test-credentials
        private const string ValidFromNumber = "+15005550006";
        private const string ValidToNumber = "+18778894546";
        private const string InvalidNumber = "+15005550001";
        private const string AvailableNumber = "+15005550006";
        private const string UnavailableNumber = "+15005550000";

        private static INotificationConfiguration GetTestConfig()
        {
            _testAccountSid =
                Environment.GetEnvironmentVariable("TWILIO_TEST_ACCOUNT_SID")
                ?? _testAccountSid;

            _testAuthToken =
                Environment.GetEnvironmentVariable("TWILIO_TEST_AUTH_TOKEN")
                ?? _testAuthToken;

            if (_testAccountSid == "ACxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx" ||
                _testAuthToken == "0123456789abcdef0123456789abcdef")
            {
                throw new Exception("You forgot to set your TestAccountSid " +
                                    "and/or TestAuthToken");
            }

            var configMock = new Mock<INotificationConfiguration>();
            configMock.SetupGet(x => x.AccountSid)
                .Returns(_testAccountSid);
            configMock.SetupGet(x => x.AuthToken)
                .Returns(_testAuthToken);
            configMock.SetupGet(x => x.DefaultFromPhoneNumber)
                .Returns(ValidFromNumber);

            return configMock.Object;
        }

        [TestMethod]
        public void SendText_Should_Send_a_Text()
        {
            var config = GetTestConfig();
            var service = new NotificationService(config);
            service.SendText(ValidToNumber, "Test message");
            // Should complete w/o exception
        }

        [TestMethod]
        [ExpectedException(typeof(NotificationException))]
        public void SendText_Should_Throw_Exception_If_Bad_Number()
        {
            var config = GetTestConfig();
            var service = new NotificationService(config);
            service.SendText(InvalidNumber, "Test message");
        }

        [TestMethod]
        public void MakePhoneCall_Should_Initiate_Phone_Call()
        {
            var config = GetTestConfig();
            var service = new NotificationService(config);
            service.MakePhoneCall(ValidToNumber, 
                "http://demo.twilio.com/docs/voice.xml");
            // Should complete w/o exception
        }

        [TestMethod]
        [ExpectedException(typeof(NotificationException))]
        public void MakePhoneCall_Should_Throw_Exception_If_Bad_Number()
        {
            var config = GetTestConfig();
            var service = new NotificationService(config);
            service.MakePhoneCall(InvalidNumber, 
                "http://demo.twilio.com/docs/voice.xml");
        }

        [TestMethod]
        public void BuyPhoneNumber_Should_Purchase_Number()
        {
            var config = GetTestConfig();
            var service = new NotificationService(config);
            service.BuyPhoneNumber(AvailableNumber);
            // Should complete w/o exception
        }

        [TestMethod]
        [ExpectedException(typeof(NotificationException))]
        public void BuyPhoneNumber_Should_Throw_Exception_If_Unavailable()
        {
            var config = GetTestConfig();
            var service = new NotificationService(config);
            service.BuyPhoneNumber(UnavailableNumber);
        }
    }
}
