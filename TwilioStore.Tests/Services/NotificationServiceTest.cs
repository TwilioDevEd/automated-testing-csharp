using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TwilioStore.Interfaces.Exceptions;
using TwilioStore.Interfaces.Services;
using TwilioStore.Services;

namespace TwilioStore.Tests.Services
{
    [TestClass]
    public class NotificationServiceTest
    {
        // Get your test credentials from https://www.twilio.com/console/account/settings
        private const string TestAccountSid = "ACxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
        private const string TestAuthToken = "0123456789abcdef0123456789abcdef";

        // Magic numbers from https://www.twilio.com/docs/api/rest/test-credentials
        private const string ValidFromNumber = "+15005550006";
        private const string ValidToNumber = "+18778894546";
        private const string InvalidNumber = "+15005550001";
        private const string AvailableNumber = "+15005550006";
        private const string UnavailableNumber = "+15005550000";

        private static INotificationConfiguration GetTestConfig()
        {
            var configMock = new Mock<INotificationConfiguration>();
            configMock.SetupGet(x => x.AccountSid)
                .Returns(TestAccountSid);
            configMock.SetupGet(x => x.AuthToken)
                .Returns(TestAuthToken);
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
            service.MakePhoneCall(ValidToNumber, "http://demo.twilio.com/docs/voice.xml");
            // Should complete w/o exception
        }

        [TestMethod]
        [ExpectedException(typeof(NotificationException))]
        public void MakePhoneCall_Should_Throw_Exception_If_Bad_Number()
        {
            var config = GetTestConfig();
            var service = new NotificationService(config);
            service.MakePhoneCall(InvalidNumber, "http://demo.twilio.com/docs/voice.xml");
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
        public void BuyPhoneNumber_Should_Throw_Exception_If_Unavailable_Number()
        {
            var config = GetTestConfig();
            var service = new NotificationService(config);
            service.BuyPhoneNumber(UnavailableNumber);
        }
    }
}
