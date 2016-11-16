using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TwilioStore.Domain;
using TwilioStore.Interfaces.Services;
using TwilioStore.Services;

namespace TwilioStore.Tests.Services
{
    [TestClass]
    public class OrderServiceTest
    {
        private const string ValidToNumber = "+18778894546";

        [TestMethod]
        public void ProcessOrder_Should_Notify_Customer()
        {
            // Arrange
            var fakeOrder = GetFakeOrder();

            var notificationServiceMock = new Mock<INotificationService>();
            string textMessage = null;
            notificationServiceMock.Setup(
                    x => x.SendText(ValidToNumber, It.IsAny<string>())
                )
                .Callback<string, string>((_, message) => textMessage = message);

            // Act
            var orderService = new OrderService(notificationServiceMock.Object);
            orderService.ProcessOrder(fakeOrder);

            // Assert
            notificationServiceMock.Verify(
                    x => x.SendText(ValidToNumber, It.IsAny<string>()),
                    Times.Once
                );
            var expectedMessageStart = $"Your order {fakeOrder.Id} has shipped! Tracking: ";
            Assert.IsTrue(textMessage.StartsWith(expectedMessageStart));
            Assert.IsTrue(textMessage.Length > expectedMessageStart.Length);
        }

        private static Order GetFakeOrder()
        {
            var fakeOrder = new Order
            {
                Id = "1234",
                Customer = new Customer
                {
                    Name = "Joe Customer",
                    MobileNumber = ValidToNumber
                }
            };
            fakeOrder.Items.Add(new OrderDetail
            {
                ItemId = "5678",
                Description = "Widget",
                Quantity = 1
            });
            return fakeOrder;
        }
    }
}
