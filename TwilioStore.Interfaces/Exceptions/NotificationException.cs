using System;

namespace TwilioStore.Interfaces.Exceptions
{
    public class NotificationException : Exception
    {
        public NotificationException(string message) : base(message)
        {
        }
    }
}